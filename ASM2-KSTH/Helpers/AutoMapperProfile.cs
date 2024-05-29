using ASM2_KSTH.Models;
using ASM2_KSTH.ViewModels;
using AutoMapper;

namespace ASM2_KSTH.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<StudentRegister, Student>()
            .ForMember(st => st.Name, option => option.MapFrom(StudentRegister => StudentRegister.Name))
            .ReverseMap();
        }

    }
}
