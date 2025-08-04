using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IBillingDetailsService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(BillingDetails billingDetails);
        Task<int> UpdateAsync(BillingDetailsUpdate billingDetailsUpdate);
        Task<int> DeleteAsync(BillingDetailsDelete billingDetailsDelete);
    }
}
