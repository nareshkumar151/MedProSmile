using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class RoomAllocationService : IRoomAllocationService
    {
        private readonly IRoomAllocationRepository _repository;

        public RoomAllocationService(IRoomAllocationRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(RoomAllocation roomAllocation)
            => _repository.CreateAsync(roomAllocation);

        public Task<int> UpdateAsync(RoomAllocationUpdate roomAllocationUpdate)
            => _repository.UpdateAsync(roomAllocationUpdate);

        public Task<int> DeleteAsync(RoomAllocationDelete roomAllocationDelete)
            => _repository.DeleteAsync(roomAllocationDelete);
    }
}
