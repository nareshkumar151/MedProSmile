using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(hospitalId, pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id, int hospitalId)
            => _repository.GetByIdAsync(id, hospitalId);

        public Task<int> CreateAsync(Patient patient)
            => _repository.CreateAsync(patient);

        public Task<int> UpdateAsync(PatientUpdateDto patientUpdateDto)
            => _repository.UpdateAsync(patientUpdateDto);

        public Task<int> DeleteAsync(PatientDeleteDto patientDeleteDto)
            => _repository.DeleteAsync(patientDeleteDto);
    }
}
