using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IAppointmentsRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id, int hospitalId);
        Task<int> CreateAsync(Appointment appointment);
        Task<int> UpdateAsync(AppointmentUpdate appointmentUpdate); 
        Task<int> DeleteAsync(AppointmentDelete appointmentDelete);
        Task<PagedResult<dynamic>> GetAllAppointmentByDoctorId(int doctorId,int pageNumber, int pageSize);
        Task<decimal?> GetConsultationFeeByDoctorAndConsultationTypeAsync(int doctorId, int consultationTypeId, int hospitalId);
        Task<IEnumerable<dynamic>> GetDoctorRevenueAsync(int? doctorId);


    }
}
