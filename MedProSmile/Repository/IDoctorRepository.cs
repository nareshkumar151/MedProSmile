using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IDoctorRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id, int hospitalId);
        Task<int> CreateAsync(Doctor doc);
        Task<int> UpdateAsync(DoctorUpdateDto doc);
        Task<int> DeleteAsync(DoctorDeleteDto doctorDeleteDto);
    }
}
