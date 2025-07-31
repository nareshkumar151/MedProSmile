using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IPatientRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Patient patient);
        Task<int> UpdateAsync(PatientUpdateDto patientUpdateDto);
        Task<int> DeleteAsync(PatientDeleteDto patientDeleteDto);
    }
}
