using AutoMapper;
using StudentManagament.DomainModels;

namespace StudentManagament.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Student>
    {
        public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAdress = source.PhysicalAdress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
