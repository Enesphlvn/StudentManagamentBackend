using StudentManagament.DataModels;

namespace StudentManagament.Repositories
{
    public interface IGenderRepository
    {
        Task<List<Gender>> GetAllAsync();
    }
}
