using AutoMapper;
using CollegeGrades.Core.Entities;
using CollegeGrades.Web.Models.TeacherViewModels;
using CollegeGrades.Web.Models.User;

namespace CollegeGrades
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.UserName, m => m.MapFrom(a => a.Email));

            CreateMap<User, ProfileViewModel>();

            CreateMap<Teacher, DisplayTeacherViewModel>();
            CreateMap<CreateTeacherViewModel, Teacher>();
        }
    }
}