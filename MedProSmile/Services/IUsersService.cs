using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IUsersService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Users users);
        Task<int> UpdateAsync(UsersUpdate usersUpdate);
        Task<int> DeleteAsync(UsersDelete usersDelete);
    }
}
