using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Assets;
using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Requests.RoomTypes;
using DormitoryManagement.Application.Dtos.Requests.Users;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Dtos.Responses.Assets;
using DormitoryManagement.Application.Dtos.Responses.Beds;
using DormitoryManagement.Application.Dtos.Responses.Blocks;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Application.Dtos.Responses.RoomTypes;
using DormitoryManagement.Application.Dtos.Requests.Utilities;
using DormitoryManagement.Application.Dtos.Responses.Utilities;
using DormitoryManagement.Application.Dtos.Requests.Vehicles;
using DormitoryManagement.Application.Dtos.Responses.Vehicles;
using DormitoryManagement.Domain.Entities;

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
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserResponseDto, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();

            // === ROOM MAPPINGS ===

            CreateMap<Room, RoomResponse>()
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.RoomType.BasePrice))
                .ForMember(dest => dest.MaxOccupants, opt => opt.MapFrom(src => src.RoomType.MaxOccupants))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.BlockId, opt => opt.MapFrom(src => src.BlockId))
                .ForMember(dest => dest.BlockName, opt => opt.MapFrom(src => src.Block != null ? src.Block.BlockName : string.Empty))
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType != null ? src.RoomType.TypeName : string.Empty))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RoomType != null ? src.RoomType.Description : string.Empty));

            // Map từ Response DTO sang Update Request (để load dữ liệu vào Form Edit)
            CreateMap<RoomResponse, UpdateRoomRequest>();

            // Map từ Request sang Entity (để lưu vào Database)
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();

            // Chi tiết kế thừa từ cơ bản
            CreateMap<Room, RoomDetailResponse>().IncludeBase<Room, RoomResponse>();

            // 3. Request -> Entity (Để lưu vào Database)
            CreateMap<CreateRoomRequest, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id tạo tự động hoặc trong Service
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<UpdateRoomRequest, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            // 4. Response -> Request (Dùng khi load dữ liệu vào Form Edit)
            CreateMap<RoomResponse, UpdateRoomRequest>();
            CreateMap<RoomDetailResponse, UpdateRoomRequest>();

            // === BED & ASSET MAPPINGS ===
            CreateMap<Bed, BedResponse>()
                .ForMember(dest => dest.IsOccupied, opt => opt.MapFrom(src => src.Status == DormitoryManagement.Domain.Enums.BedStatus.Occupied));

            CreateMap<Asset, AssetResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room != null ? src.Room.RoomNumber : string.Empty))
                .ForMember(dest => dest.BlockName, opt => opt.MapFrom(src => src.Room != null && src.Room.Block != null ? src.Room.Block.BlockName : string.Empty));

            CreateMap<CreateAssetRequest, Asset>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Room, opt => opt.Ignore());

            CreateMap<UpdateAssetRequest, Asset>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Room, opt => opt.Ignore());

            CreateMap<AssetResponse, UpdateAssetRequest>();

            // === BLOCK MAPPINGS ===
            CreateMap<Block, BlockResponseDto>()
                .ForMember(dest => dest.TotalRooms, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count(r => !r.IsDeleted) : 0));

            CreateMap<BlockRequestDto, Block>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rooms, opt => opt.Ignore());

            // === ROOM TYPE MAPPINGS ===
            CreateMap<RoomType, RoomTypeResponseDto>();
            CreateMap<RoomTypeRequestDto, RoomType>();

            // === VEHICLE MAPPINGS ===
            CreateMap<Vehicle, VehicleResponseDto>()
                .ForMember(dest => dest.OwnerFullName, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.FullName : string.Empty))
                .ForMember(dest => dest.OwnerCode, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.Code : string.Empty));

            CreateMap<VehicleRequestDto, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Owner, opt => opt.Ignore());

            CreateMap<VehicleUpdateDto, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore());

            // === UTILITY MAPPINGS ===
            CreateMap<Utility, UtilityResponseDto>();
            CreateMap<UtilityRequestDto, Utility>();
            CreateMap<UtilityResponseDto, UtilityRequestDto>();
        }
    }
}
