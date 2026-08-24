using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _repository;

        public AppointmentsService(IAppointmentsRepository repository) 
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(hospitalId, pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id, int hospitalId)
            => _repository.GetByIdAsync(id, hospitalId);

        public Task<int> CreateAsync(Appointment appointment)
            => _repository.CreateAsync(appointment);

        public Task<int> UpdateAsync(AppointmentUpdate appointmentUpdate)
            => _repository.UpdateAsync(appointmentUpdate);

        public Task<int> DeleteAsync(AppointmentDelete appointmentDelete)
            => _repository.DeleteAsync(appointmentDelete);
        public Task<PagedResult<dynamic>> GetAllAppointmentByDoctorId(int doctorId, int pageNumber, int pageSize)
        => _repository.GetAllAppointmentByDoctorId(doctorId,pageNumber, pageSize);

        public Task<decimal?> GetConsultationFeeByDoctorAndConsultationTypeAsync(int doctorId, int consultationTypeId, int hospitalId)
        => _repository.GetConsultationFeeByDoctorAndConsultationTypeAsync(doctorId, consultationTypeId, hospitalId);

        public Task<IEnumerable<dynamic>> GetDoctorRevenueAsync(int? doctorId)
        => _repository.GetDoctorRevenueAsync(doctorId);

    }
}
