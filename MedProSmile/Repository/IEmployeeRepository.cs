using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IEmployeeRepository
    {
        Task<PagedResult<Employee>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<Employee> GetByIdAsync(int id);
        Task<int> CreateAsync(Employee emp);
        Task<int> UpdateAsync(Employee emp);
        Task<int> DeleteAsync(int id);
    }
}
