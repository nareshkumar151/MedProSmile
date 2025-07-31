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

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Patient patient)
            => _repository.CreateAsync(patient);

        public Task<int> UpdateAsync(PatientUpdateDto patientUpdateDto)
            => _repository.UpdateAsync(patientUpdateDto);

        public Task<int> DeleteAsync(PatientDeleteDto patientDeleteDto)
            => _repository.DeleteAsync(patientDeleteDto);
    }
}
