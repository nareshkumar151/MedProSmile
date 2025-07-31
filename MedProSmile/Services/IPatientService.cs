using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IPatientService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Patient patient);
        Task<int> UpdateAsync(PatientUpdateDto patientUpdateDto);
        Task<int> DeleteAsync(PatientDeleteDto patientDeleteDto);
    }
}
