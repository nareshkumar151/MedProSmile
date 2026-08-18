using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IDoctorConsultantFeeRepository
    {
        Task<int> CreateAsync(DoctorConsultantFeeModel model);
        Task<int> UpdateAsync(DoctorConsultantFeeModel model);
        Task<int> DeleteAsync(int consultantFeeId, int? updatedBy);
        Task<IEnumerable<DoctorConsultantFeeModel>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<DoctorConsultantFeeModel>> GetAllAsync();
    }
}
