using Austistic.Core.DTOs.Request.Auth;
using Austistic.Core.DTOs.Response.Auth;
using Austistic.Core.Entities;
using AutoMapper;


namespace AlpaStock.Api.MapingProfile
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ApplicationUser, DisplayFindUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();

        }
    }
}