using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class BillingMasterService : IBillingMasterService
    {
        private readonly IBillingMasterRepository _repository;

        public BillingMasterService(IBillingMasterRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(BillingMaster billingMaster)
            => _repository.CreateAsync(billingMaster);

        public Task<int> UpdateAsync(BillingMasterUpdate billingMasterUpdate)
            => _repository.UpdateAsync(billingMasterUpdate);

        public Task<int> DeleteAsync(BillingMasterDelete billingMasterDelete)
            => _repository.DeleteAsync(billingMasterDelete);
    }
}
