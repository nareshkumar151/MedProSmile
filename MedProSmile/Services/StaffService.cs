using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repository;

        public StaffService(IStaffRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Staff staff)
            => _repository.CreateAsync(staff);

        public Task<int> UpdateAsync(StaffUpdate staffUpdate)
            => _repository.UpdateAsync(staffUpdate);

        public Task<int> DeleteAsync(StaffDelete staffDelete)
            => _repository.DeleteAsync(staffDelete);
    }
}
