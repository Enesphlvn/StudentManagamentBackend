using StudentManagament.DataModels;

namespace StudentManagament.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(Guid studentId);
    }
}
