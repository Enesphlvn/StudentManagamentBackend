using Microsoft.EntityFrameworkCore;
using StudentManagament.DataModels;

namespace StudentManagament.Repositories
{
    public class SqlGenderRepository : IGenderRepository
    {
        private readonly StudentAdminContext _studentAdminContext;

        public SqlGenderRepository(StudentAdminContext studentAdminContext)
        {
            _studentAdminContext = studentAdminContext;
        }

        public async Task<List<Gender>> GetAllAsync()
        {
            return await _studentAdminContext.Gender.ToListAsync();
        }
    }
}
