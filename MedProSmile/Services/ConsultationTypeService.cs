using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class ConsultationTypeService : IConsultationTypeService
    {
        private readonly IConsultationTypeRepository _repository;

        public ConsultationTypeService(IConsultationTypeRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ConsultationTypeModel>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<ConsultationTypeModel> GetByIdAsync(int consultationTypeId)
            => _repository.GetByIdAsync(consultationTypeId);

        public Task<int> CreateAsync(ConsultationTypeModel model)
            => _repository.CreateAsync(model);

        public Task<int> UpdateAsync(ConsultationTypeModel model)
            => _repository.UpdateAsync(model);

        public Task<int> DeleteAsync(int consultationTypeId)
            => _repository.DeleteAsync(consultationTypeId);
    }
}
