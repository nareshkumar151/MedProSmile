using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IHospitalService
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Hospital hsp);
        Task<int> UpdateAsync(HospitalUpdateDto hsp);
        Task<int> DeleteAsync(HospitalDeleteDto hospitalDeleteDto);
    }
}
