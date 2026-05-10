using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
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
        }
    }

}
