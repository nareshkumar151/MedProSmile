using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class BillingDetailsService : IBillingDetailsService
    {
        private readonly IBillingDetailsRepository _repository;

        public BillingDetailsService(IBillingDetailsRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(BillingDetails billingDetails)
            => _repository.CreateAsync(billingDetails);

        public Task<int> UpdateAsync(BillingDetailsUpdate billingDetailsUpdate)
            => _repository.UpdateAsync(billingDetailsUpdate);

        public Task<int> DeleteAsync(BillingDetailsDelete billingDetailsDelete)
            => _repository.DeleteAsync(billingDetailsDelete);
    }
}
