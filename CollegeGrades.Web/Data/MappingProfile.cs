using AutoMapper;
using CollegeGrades.Core.Entities;
using CollegeGrades.Web.Models.User;

namespace CollegeGrades.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>();
            CreateMap<User, DisplayUserViewModel>();
        }
    }
}