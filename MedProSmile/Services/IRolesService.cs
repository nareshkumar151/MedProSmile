using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IRolesService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int RoleId);
        Task<int> CreateAsync(string RoleName);
        Task<int> UpdateAsync(int RoleId, string RoleName);
        Task<int> DeleteAsync(int RoleId);
    }
}
