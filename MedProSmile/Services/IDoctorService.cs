using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IDoctorService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id, int hospitalId);
        Task<int> CreateAsync(Doctor doc);
        Task<int> UpdateAsync(DoctorUpdateDto doc);
        Task<int> DeleteAsync(DoctorDeleteDto doctorDeleteDto);
    }
}
