using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IPatientMedicinesRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(PatientMedicine patientMedicine);
        Task<int> UpdateAsync(PatientMedicineUpdate patientMedicineUpdate);
        Task<int> DeleteAsync(PatientMedicineDelete patientMedicineDelete);
    }
}
