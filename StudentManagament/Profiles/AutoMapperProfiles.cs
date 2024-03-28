using AutoMapper;
using DataModels = StudentManagament.DataModels;
using StudentManagament.DomainModels;

namespace StudentManagament.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, Student>().ReverseMap();
            CreateMap<DataModels.Gender, Gender>().ReverseMap();
            CreateMap<DataModels.Address, Address>().ReverseMap();
        }
    }
}
