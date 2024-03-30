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

        public async Task<bool> Exists(Guid studentId)
        {
            return await _studentAdminContext.Student.AnyAsync(s => s.Id == studentId);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentAdminContext.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(Guid studentId)
        {
            return await _studentAdminContext.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(s => s.Id == studentId);
        }

        public async Task<Student> UpdateAsync(Guid studentId, Student student)
        {
            var existingStudent = await GetByIdAsync(studentId);

            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Email = student.Email;
                existingStudent.Mobile = student.Mobile;
                existingStudent.GenderId = student.GenderId;
                existingStudent.Address.PhysicalAdress = student.Address.PhysicalAdress;
                existingStudent.Address.PostalAddress = student.Address.PostalAddress;

                await _studentAdminContext.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }
    }
}
