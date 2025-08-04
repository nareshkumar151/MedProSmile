using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private readonly IDischargeSummaryRepository _repository;

        public DischargeSummaryService(IDischargeSummaryRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(DischargeSummary dischargeSummary)
            => _repository.CreateAsync(dischargeSummary);

        public Task<int> UpdateAsync(DischargeSummaryUpdate dischargeSummaryUpdate)
            => _repository.UpdateAsync(dischargeSummaryUpdate);

        public Task<int> DeleteAsync(DischargeSummaryDelete dischargeSummaryDelete)
            => _repository.DeleteAsync(dischargeSummaryDelete);
    }
}
