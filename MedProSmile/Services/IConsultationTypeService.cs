using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IConsultationTypeService
    {
        Task<IEnumerable<ConsultationTypeModel>> GetAllAsync();
        Task<ConsultationTypeModel> GetByIdAsync(int consultationTypeId);
        Task<int> CreateAsync(ConsultationTypeModel model);
        Task<int> UpdateAsync(ConsultationTypeModel model);
        Task<int> DeleteAsync(int consultationTypeId);
    }
}
