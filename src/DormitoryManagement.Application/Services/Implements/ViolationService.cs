using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Requests.Violations;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý vi phạm kỷ luật (ViolationService - Sử dụng Database EF Core).
    /// </summary>
    public class ViolationService(
        IViolationRepository violationRepository,
        IContractRepository contractRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IViolationService
    {
        private readonly IViolationRepository _violationRepository = violationRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Lấy danh sách vi phạm kỷ luật kèm theo phân trang và tìm kiếm từ database.
        /// </summary>
        public async Task<PagedResult<ViolationResponseDto>> GetActiveViolationsPagedAsync(int page, int pageSize, string search)
        {
            var query = _violationRepository.GetQuery()
                .Include(v => v.Contract)
                    .ThenInclude(c => c.User)
                .Include(v => v.Contract)
                    .ThenInclude(c => c.Bed)
                        .ThenInclude(b => b.Room)
                            .ThenInclude(r => r.Block)
                .Where(v => !v.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower().Trim();
                query = query.Where(v => v.Contract.User != null &&
                        (v.Contract.User.Code.ToLower().Contains(lowerSearch)
                         || v.Contract.User.FullName.ToLower().Contains(lowerSearch)
                         || v.Contract.Bed.Room.RoomNumber.ToLower().Contains(lowerSearch)
                         || v.Description.ToLower().Contains(lowerSearch)));
            }

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(v => v.ViolationDate)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            var dtos = _mapper.Map<List<ViolationResponseDto>>(items);
            return new PagedResult<ViolationResponseDto>(dtos, totalCount, page, pageSize);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một bản ghi vi phạm kỷ luật của sinh viên theo Id từ database.
        /// </summary>
        public async Task<ViolationResponseDto?> GetViolationByIdAsync(Guid id)
        {
            var violation = await _violationRepository.GetQuery()
                .Include(v => v.Contract)
                    .ThenInclude(c => c.User)
                .Include(v => v.Contract)
                    .ThenInclude(c => c.Bed)
                        .ThenInclude(b => b.Room)
                            .ThenInclude(r => r.Block)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);

            if (violation == null) return null;

            return _mapper.Map<ViolationResponseDto>(violation);
        }

        /// <summary>
        /// Tạo mới một bản ghi vi phạm kỷ luật vào database.
        /// </summary>
        public async Task<bool> CreateViolationAsync(ViolationRequestDto violationDto)
        {
            // Tìm sinh viên theo mã số (Code)
            var student = await _userRepository.GetQuery()
                    .FirstOrDefaultAsync(u => u.Code == violationDto.StudentId && !u.IsDeleted)
                    ?? throw new Exception($"Không tìm thấy sinh viên với mã số '{violationDto.StudentId}' trong hệ thống.");

            // Tìm hợp đồng ở trạng thái Active của sinh viên
            var contract = await _contractRepository.GetQuery()
                .FirstOrDefaultAsync(c => c.UserId == student.Id && c.Status == ContractStatus.Active && !c.IsDeleted)
                ?? throw new Exception($"Sinh viên {student.FullName} ({violationDto.StudentId}) hiện tại không có hợp đồng thuê phòng ở trạng thái hoạt động.");

            // Xác định số tiền phạt dựa trên mức độ vi phạm
            decimal fineAmount = violationDto.Severity switch
            {
                "Nhẹ" => 50000m,
                "Trung bình" => 100000m,
                "Nghiêm trọng" => 200000m,
                "Cảnh cáo" => 300000m,
                _ => 50000m
            };

            var violation = new Violation
            {
                Id = Guid.NewGuid(),
                ContractId = contract.Id,
                Description = violationDto.Content,
                FineAmount = fineAmount,
                ViolationDate = violationDto.Date,
                Status = violationDto.Status == "Đã xử lý" ? ViolationStatus.Resolved : ViolationStatus.Pending,
                EvidenceImage = "/images/violations/default_evidence.jpg",
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await _violationRepository.AddAsync(violation);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin bản ghi vi phạm kỷ luật trong database.
        /// </summary>
        public async Task<bool> UpdateViolationAsync(Guid id, ViolationRequestDto violationDto)
        {
            var violation = await _violationRepository.GetByIdAsync(id);
            if (violation == null) return false;

            // Cập nhật mô tả lỗi, ngày lập và trạng thái xử lý
            violation.Description = violationDto.Content;
            violation.ViolationDate = violationDto.Date;
            violation.Status = violationDto.Status == "Đã xử lý" ? ViolationStatus.Resolved : ViolationStatus.Pending;

            // Cập nhật lại số tiền phạt dựa trên mức độ vi phạm mới chọn
            violation.FineAmount = violationDto.Severity switch
            {
                "Nhẹ" => 50000m,
                "Trung bình" => 100000m,
                "Nghiêm trọng" => 200000m,
                "Cảnh cáo" => 300000m,
                _ => violation.FineAmount
            };

            await _violationRepository.UpdateAsync(violation);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa mềm một bản ghi vi phạm kỷ luật.
        /// </summary>
        public async Task<bool> DeleteViolationAsync(Guid id)
        {
            var violation = await _violationRepository.GetByIdAsync(id);
            if (violation == null) return false;

            await _violationRepository.DeleteAsync(violation, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Lấy toàn bộ vi phạm kỷ luật của một sinh viên theo UserId.
        /// Dùng ContractId làm cầu nối: lấy tất cả hợp đồng của sinh viên,
        /// sau đó lấy vi phạm liên quan đến từng hợp đồng đó.
        /// </summary>
        public async Task<IEnumerable<ViolationResponseDto>> GetViolationsByUserIdAsync(Guid userId)
        {
            // Lấy tất cả hợp đồng của sinh viên (kể cả hết hạn)
            var contracts = await _contractRepository.GetQuery()
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .Select(c => c.Id)
                .ToListAsync();

            if (contracts.Count is 0)
                return [];

            // Lấy vi phạm của tất cả hợp đồng, kèm thông tin Contract -> User, Bed -> Room -> Block
            var violations = await _violationRepository.GetQuery()
                .Include(v => v.Contract)
                    .ThenInclude(c => c.User)
                .Include(v => v.Contract)
                    .ThenInclude(c => c.Bed)
                        .ThenInclude(b => b.Room)
                            .ThenInclude(r => r.Block)
                .Where(v => !v.IsDeleted && contracts.Contains(v.ContractId))
                .OrderByDescending(v => v.ViolationDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ViolationResponseDto>>(violations);
        }

        /// <summary>
        /// Xử lý (giải quyết) một biên bản vi phạm: chuyển trạng thái sang Resolved,
        /// ghi nhận ghi chú xử lý và thời gian xử lý xong.
        /// </summary>
        public async Task<bool> ResolveViolationAsync(Guid id, string resolveNote)
        {
            var violation = await _violationRepository.GetByIdAsync(id);
            if (violation == null) return false;

            // Cập nhật trạng thái và thông tin xử lý
            violation.Status = ViolationStatus.Resolved;
            violation.ResolveNote = resolveNote?.Trim();
            violation.ResolvedAt = DateTime.Now;
            violation.LastModified = DateTime.Now;

            await _violationRepository.UpdateAsync(violation);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Lấy danh sách vi phạm đã bị xóa phân trang.
        /// </summary>
        public async Task<PagedResult<ViolationResponseDto>> GetDeletedViolationsPagedAsync(int page, int pageSize, string search)
        {
            Expression<Func<Violation, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower().Trim();
                predicate = v => v.Contract.User != null &&
                        (v.Contract.User.Code.ToLower().Contains(lowerSearch)
                         || v.Contract.User.FullName.ToLower().Contains(lowerSearch)
                         || v.Contract.Bed.Room.RoomNumber.ToLower().Contains(lowerSearch)
                         || v.Description.ToLower().Contains(lowerSearch));
            }

            var pagedData = await _violationRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: null,
                isDeleted: true,
                predicate: predicate,
                v => v.Contract!,
                v => v.Contract!.User!,
                v => v.Contract!.Bed!,
                v => v.Contract!.Bed!.Room!,
                v => v.Contract!.Bed!.Room!.Block!);

            var dtos = _mapper.Map<List<ViolationResponseDto>>(pagedData.Items);
            return new PagedResult<ViolationResponseDto>(dtos, pagedData.TotalCount, page, pageSize);
        }

        /// <summary>
        /// Khôi phục vi phạm đã bị xóa mềm.
        /// </summary>
        public async Task<bool> RestoreViolationAsync(Guid id)
        {
            var violation = await _violationRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (violation == null || !violation.IsDeleted) return false;

            await _violationRepository.RestoreAsync(violation);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa vĩnh viễn vi phạm khỏi database.
        /// </summary>
        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var violation = await _violationRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (violation == null) return false;

            await _violationRepository.DeleteAsync(violation, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}