using AutoMapper;
using EmployeeManagement.Dtos;
using EmployeeManagement.Models;
using EmployeeManagement.Identity;

namespace EmployeeManagement.AutoMapperHelper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

        }
    }
}
