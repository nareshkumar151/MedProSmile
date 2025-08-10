using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IStaffRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Staff staff);
        Task<int> UpdateAsync(StaffUpdate staffUpdate);
        Task<int> DeleteAsync(StaffDelete staffDelete);
    }
}
