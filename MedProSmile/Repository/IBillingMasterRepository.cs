using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IBillingMasterRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(BillingMaster billingMaster);
        Task<int> UpdateAsync(BillingMasterUpdate billingMasterUpdate);
        Task<int> DeleteAsync(BillingMasterDelete billingMasterDelete);
    }
}
