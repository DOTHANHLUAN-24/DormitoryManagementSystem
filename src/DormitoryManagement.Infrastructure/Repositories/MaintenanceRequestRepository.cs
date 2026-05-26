using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class MaintenanceRequestRepository(ApplicationDbContext db) : BaseRepository<MaintenanceRequest>(db), IMaintenanceRequestRepository
    {

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByRoomIdAsync(Guid roomId)
        {
            return await _dbSet.AsNoTracking().Where(m => m.RoomId == roomId && !m.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByRequesterIdAsync(Guid requesterId)
        {
            return await _dbSet.AsNoTracking().Where(m => m.RequesterId == requesterId && !m.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByHandlerIdAsync(Guid handlerId)
        {
            return await _dbSet.AsNoTracking().Where(m => m.HandlerId == handlerId && !m.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByStatusAsync(MaintenanceStatus status)
        {
            return await _dbSet.AsNoTracking().Where(m => m.Status == status && !m.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetRequestsByPriorityAsync(MaintenancePriority priority)
        {
            return await _dbSet.AsNoTracking().Where(m => m.Priority == priority && !m.IsDeleted).ToListAsync();
        }
    }
}
