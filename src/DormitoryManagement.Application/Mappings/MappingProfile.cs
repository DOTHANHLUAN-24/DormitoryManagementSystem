using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Requests.RoomTypes;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Dtos.Responses.Assets;
using DormitoryManagement.Application.Dtos.Responses.Beds;
using DormitoryManagement.Application.Dtos.Responses.Blocks;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Application.Dtos.Responses.RoomTypes;
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

            // === ROOM MAPPINGS ===

            // 1. Entity -> RoomResponse (Dùng cho danh sách)
            CreateMap<Room, RoomResponse>()
                .ForMember(dest => dest.BlockName, opt => opt.MapFrom(src => src.Block.BlockName))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.TypeName))
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.RoomType.BasePrice))
                .ForMember(dest => dest.MaxOccupants, opt => opt.MapFrom(src => src.RoomType.MaxOccupants));

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
            CreateMap<Bed, BedResponse>();
            CreateMap<Asset, AssetResponse>();

            // === BLOCK MAPPINGS ===
            CreateMap<Block, BlockResponseDto>()
                .ForMember(dest => dest.RoomCount, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count : 0));
            CreateMap<BlockRequestDto, Block>();

            // === ROOM TYPE MAPPINGS ===
            CreateMap<RoomType, RoomTypeResponseDto>();
            CreateMap<RoomTypeRequestDto, RoomType>();
        }
    }
}