using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Employee emp)
            => _repository.CreateAsync(emp);

        public Task<int> UpdateAsync(Employee emp)
            => _repository.UpdateAsync(emp);

        public Task<int> DeleteAsync(int id)
            => _repository.DeleteAsync(id);
    }
}
