using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Doctor doc)
            => _repository.CreateAsync(doc);

        public Task<int> UpdateAsync(DoctorUpdateDto doc)
            => _repository.UpdateAsync(doc);

        public Task<int> DeleteAsync(DoctorDeleteDto doctorDeleteDto)
            => _repository.DeleteAsync(doctorDeleteDto);
    }
}
