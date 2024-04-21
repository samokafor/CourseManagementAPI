using AutoMapper;
using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;

namespace SchoolManagementAPI.DTOs
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UpdateUserRoleDto>();
            CreateMap<UserLogin, UserLoginDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Instructor, InstructorDto>();
            
        }
    }

}
