using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IDoctorRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Doctor doc);
        Task<int> UpdateAsync(DoctorUpdateDto doc);
        Task<int> DeleteAsync(DoctorDeleteDto doctorDeleteDto);
    }
}
