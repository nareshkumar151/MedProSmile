using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IDischargeSummaryRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(DischargeSummary dischargeSummary);
        Task<int> UpdateAsync(DischargeSummaryUpdate dischargeSummaryUpdate);
        Task<int> DeleteAsync(DischargeSummaryDelete dischargeSummaryDelete);
    }
}
