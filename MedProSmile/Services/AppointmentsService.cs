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

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Appointment appointment)
            => _repository.CreateAsync(appointment);

        public Task<int> UpdateAsync(AppointmentUpdate appointmentUpdate)
            => _repository.UpdateAsync(appointmentUpdate);

        public Task<int> DeleteAsync(AppointmentDelete appointmentDelete)
            => _repository.DeleteAsync(appointmentDelete);
    }
}
