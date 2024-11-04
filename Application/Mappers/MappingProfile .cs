using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Contact, GetContactDto>();
            CreateMap<Contact, ContactDto>();
        }
    }
}
