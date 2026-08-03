using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IMedicalRecordsRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int? doctorId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(MedicalRecords medicalRecords);
        Task<int> UpdateAsync(MedicalRecordUpdate medicalRecordUpdate);
        Task<int> DeleteAsync(MedicalRecordDelete medicalRecordDelete);
    }
}
