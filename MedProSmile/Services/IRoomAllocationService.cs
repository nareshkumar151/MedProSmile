using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IRoomAllocationService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(RoomAllocation roomAllocation);
        Task<int> UpdateAsync(RoomAllocationUpdate roomAllocationUpdate);
        Task<int> DeleteAsync(RoomAllocationDelete roomAllocationDelete);
    }
}
