using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IAppointmentsRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Appointment appointment);
        Task<int> UpdateAsync(AppointmentUpdate appointmentUpdate);
        Task<int> DeleteAsync(AppointmentDelete appointmentDelete);
    }
}
