using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IDoctorService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Doctor doc);
        Task<int> UpdateAsync(DoctorUpdateDto doc);
        Task<int> DeleteAsync(DoctorDeleteDto doctorDeleteDto);
    }
}
