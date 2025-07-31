using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repository;

        public HospitalService(IHospitalRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Hospital hsp)
            => _repository.CreateAsync(hsp);

        public Task<int> UpdateAsync(HospitalUpdateDto hsp)
            => _repository.UpdateAsync(hsp);

        public Task<int> DeleteAsync(HospitalDeleteDto hospitalDeleteDto)
            => _repository.DeleteAsync(hospitalDeleteDto);
    }
}
