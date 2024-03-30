using AutoMapper;
using StudentManagament.DomainModels;
using DataModels = StudentManagament.DataModels;

namespace StudentManagament.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModels.Student>
    {
        public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Address()
            {
                PhysicalAdress = source.PhysicalAdress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
