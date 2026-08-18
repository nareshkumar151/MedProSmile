using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IDoctorConsultantFeeService
    {
        Task<int> CreateAsync(DoctorConsultantFeeModel model);
        Task<int> UpdateAsync(DoctorConsultantFeeModel model);
        Task<int> DeleteAsync(int consultantFeeId, int? updatedBy);
        Task<IEnumerable<DoctorConsultantFeeModel>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<DoctorConsultantFeeModel>> GetAllAsync();
    }
}
