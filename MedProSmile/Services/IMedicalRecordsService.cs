using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IMedicalRecordsService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int? doctorId, int hospitalId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id, int hospitalId);
        Task<int> CreateAsync(MedicalRecords medicalRecords);
        Task<int> UpdateAsync(MedicalRecordUpdate medicalRecordUpdate);
        Task<int> DeleteAsync(MedicalRecordDelete medicalRecordDelete);
    }
}
