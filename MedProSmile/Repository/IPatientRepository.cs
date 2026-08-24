using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IPatientRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id, int hospitalId);
        Task<int> CreateAsync(Patient patient);
        Task<int> UpdateAsync(PatientUpdateDto patientUpdateDto);
        Task<int> DeleteAsync(PatientDeleteDto patientDeleteDto);
    }
}
