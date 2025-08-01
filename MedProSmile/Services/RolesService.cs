using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _repository;

        public RolesService(IRolesRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int RoleId)
            => _repository.GetByIdAsync(RoleId);

        public Task<int> CreateAsync( string RoleName)
            => _repository.CreateAsync(RoleName);

        public Task<int> UpdateAsync(int RoleId, string RoleName)
            => _repository.UpdateAsync(RoleId,RoleName);

        public Task<int> DeleteAsync(int RoleId)
            => _repository.DeleteAsync(RoleId);
    }
}
