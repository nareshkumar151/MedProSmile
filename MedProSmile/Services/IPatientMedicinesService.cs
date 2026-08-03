using MedProSmile.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Services
{
    public interface IPatientMedicinesService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int? doctorId,int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(PatientMedicine patientMedicine);
        Task<int> UpdateAsync(PatientMedicineUpdate patientMedicineUpdate);
        Task<int> DeleteAsync(PatientMedicineDelete patientMedicineDelete);
    }
}
