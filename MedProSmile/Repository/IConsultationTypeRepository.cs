using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IConsultationTypeRepository
    {
        Task<IEnumerable<ConsultationTypeModel>> GetAllAsync();
        Task<ConsultationTypeModel> GetByIdAsync(int consultationTypeId);
        Task<int> CreateAsync(ConsultationTypeModel model);
        Task<int> UpdateAsync(ConsultationTypeModel model);
        Task<int> DeleteAsync(int consultationTypeId);
    }
}
