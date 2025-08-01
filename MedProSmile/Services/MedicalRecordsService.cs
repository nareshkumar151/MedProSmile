using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly IMedicalRecordsRepository _repository;

        public MedicalRecordsService(IMedicalRecordsRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(MedicalRecords medicalRecords)
            => _repository.CreateAsync(medicalRecords);

        public Task<int> UpdateAsync(MedicalRecordUpdate medicalRecordUpdate)
            => _repository.UpdateAsync(medicalRecordUpdate);

        public Task<int> DeleteAsync(MedicalRecordDelete medicalRecordDelete)
            => _repository.DeleteAsync(medicalRecordDelete);
    }
}
