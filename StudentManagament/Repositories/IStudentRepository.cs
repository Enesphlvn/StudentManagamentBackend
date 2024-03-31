using StudentManagament.DataModels;

namespace StudentManagament.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(Guid studentId);
        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateAsync(Guid studentId, Student student);
        Task<Student> DeleteAsync(Guid studentId);
    }
}
