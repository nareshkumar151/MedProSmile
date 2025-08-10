using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class DischargeBillingService : IDischargeBillingService
    {
        private readonly IDischargeBillingRepository _repository;

        public DischargeBillingService(IDischargeBillingRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(DischargeBilling dischargeBilling)
            => _repository.CreateAsync(dischargeBilling);

        public Task<int> UpdateAsync(DischargeBillingUpdate dischargeBillingUpdate)
            => _repository.UpdateAsync(dischargeBillingUpdate);

        public Task<int> DeleteAsync(DischargeBillingDelete dischargeBillingDelete)
            => _repository.DeleteAsync(dischargeBillingDelete);
    }
}
