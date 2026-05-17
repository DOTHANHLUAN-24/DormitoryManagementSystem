using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // === USER MAPPINGS ===
            CreateMap<User, UserResponseDto>();
            CreateMap<UserRequestDto, User>();
            CreateMap<UserResponseDto, UserRequestDto>();

            // === ROOM MAPPINGS ===
            // Entity -> Response
            CreateMap<Room, RoomResponse>()
                // Map các thuộc tính cơ bản (AutoMapper tự map nếu cùng tên, 
                // nhưng ID và Status nên viết rõ nếu có xử lý)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))

                // Chuyển Enum Status sang String (Ví dụ: Available -> "Available")
                // Nếu bạn có hàm Helper để lấy Description của Enum thì dùng ở đây
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))

                // Map từ Block (Lưu ý kiểm tra null để tránh lỗi)
                .ForMember(dest => dest.BlockId, opt => opt.MapFrom(src => src.BlockId))
                .ForMember(dest => dest.BlockName, opt => opt.MapFrom(src => src.Block != null ? src.Block.BlockName : string.Empty))

                // Map từ RoomType
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType != null ? src.RoomType.ToString() : string.Empty));

            // Map từ Response DTO sang Update Request (để load dữ liệu vào Form Edit)
            CreateMap<RoomResponse, UpdateRoomRequest>();

            // Map từ Request sang Entity (để lưu vào Database)
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();

            CreateMap<Room, RoomDetailResponse>().IncludeBase<Room, RoomResponse>();
            CreateMap<Bed, BedResponse>();
            CreateMap<Asset, AssetResponse>();

            // Request -> Entity
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();
        }
    }

}
