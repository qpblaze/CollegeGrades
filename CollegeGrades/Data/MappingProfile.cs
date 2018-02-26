using AutoMapper;
using CollegeGrades.Models;
using CollegeGrades.Models.AccountViewModels;

namespace CollegeGrades.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogInViewModel, Account>();
            CreateMap<RegisterViewModel, Account>();
        }
    }
}