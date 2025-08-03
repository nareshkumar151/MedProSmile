using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class PatientMedicinesService : IPatientMedicinesService
    {
        private readonly IPatientMedicinesRepository _repository;

        public PatientMedicinesService(IPatientMedicinesRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(PatientMedicine patientMedicine)
            => _repository.CreateAsync(patientMedicine);

        public Task<int> UpdateAsync(PatientMedicineUpdate patientMedicineUpdate)
            => _repository.UpdateAsync(patientMedicineUpdate);

        public Task<int> DeleteAsync(PatientMedicineDelete patientMedicineDelete)
            => _repository.DeleteAsync(patientMedicineDelete);
    }
}
