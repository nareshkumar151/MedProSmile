using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllDepartmentCategories()
            => _repository.GetAllDepartmentCategories();

        public Task<dynamic> GetDepartmentCategoryById(int id)
            => _repository.GetDepartmentCategoryById(id);

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Department dep)
            => _repository.CreateAsync(dep);

        public Task<int> UpdateAsync(Department dep)
            => _repository.UpdateAsync(dep);

        public Task<PagedResult<dynamic>> GetAllStates()
             => _repository.GetAllStates();

        public Task<PagedResult<dynamic>> GetCitiesByStateId(int StateId)
             => _repository.GetCitiesByStateId(StateId);

    }
}
