using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IStaffService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Staff staff);
        Task<int> UpdateAsync(StaffUpdate staffUpdate);
        Task<int> DeleteAsync(StaffDelete staffDelete);
    }
}
