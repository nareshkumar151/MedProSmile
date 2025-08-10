using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IDischargeBillingRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(DischargeBilling dischargeBilling);
        Task<int> UpdateAsync(DischargeBillingUpdate dischargeBillingUpdate);
        Task<int> DeleteAsync(DischargeBillingDelete dischargeBillingDelete);
    }
}
