using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IEmployeeService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Employee emp);
        Task<int> UpdateAsync(Employee emp);
        Task<int> DeleteAsync(int id);
    }
}
