using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IMedicalRecordsRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int? doctorId, int hospitalId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id, int hospitalId);
        Task<int> CreateAsync(MedicalRecords medicalRecords);
        Task<int> UpdateAsync(MedicalRecordUpdate medicalRecordUpdate);
        Task<int> DeleteAsync(MedicalRecordDelete medicalRecordDelete);
    }
}
