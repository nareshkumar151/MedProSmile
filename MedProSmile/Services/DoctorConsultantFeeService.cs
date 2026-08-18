using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class DoctorConsultantFeeService : IDoctorConsultantFeeService
    {
        private readonly IDoctorConsultantFeeRepository _repository;

        public DoctorConsultantFeeService(IDoctorConsultantFeeRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CreateAsync(DoctorConsultantFeeModel model)
            => _repository.CreateAsync(model);

        public Task<int> UpdateAsync(DoctorConsultantFeeModel model)
            => _repository.UpdateAsync(model);

        public Task<int> DeleteAsync(int consultantFeeId, int? updatedBy)
            => _repository.DeleteAsync(consultantFeeId, updatedBy);

        public Task<IEnumerable<DoctorConsultantFeeModel>> GetByDoctorIdAsync(int doctorId)
            => _repository.GetByDoctorIdAsync(doctorId);

        public Task<IEnumerable<DoctorConsultantFeeModel>> GetAllAsync()
            => _repository.GetAllAsync();
    }
}
