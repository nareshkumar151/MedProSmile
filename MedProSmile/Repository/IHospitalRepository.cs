using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IHospitalRepository
    {
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Hospital hsp);
        Task<int> UpdateAsync(HospitalUpdateDto hsp);
        Task<int> DeleteAsync(HospitalDeleteDto hospitalDeleteDto);
    }
}
