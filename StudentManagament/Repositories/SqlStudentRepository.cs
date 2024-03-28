using Microsoft.EntityFrameworkCore;
using StudentManagament.DataModels;

namespace StudentManagament.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext _studentAdminContext;

        public SqlStudentRepository(StudentAdminContext studentAdminContext)
        {
            _studentAdminContext = studentAdminContext;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentAdminContext.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(Guid studentId)
        {
            return await _studentAdminContext.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(s => s.Id == studentId);
        }
    }
}
