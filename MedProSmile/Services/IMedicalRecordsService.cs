using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IMedicalRecordsService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(MedicalRecords medicalRecords);
        Task<int> UpdateAsync(MedicalRecordUpdate medicalRecordUpdate);
        Task<int> DeleteAsync(MedicalRecordDelete medicalRecordDelete);
    }
}
