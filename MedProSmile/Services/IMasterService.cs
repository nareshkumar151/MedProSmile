using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IMasterService
    {
        Task<PagedResult<dynamic>> GetAllDepartmentCategories();
        Task<dynamic> GetDepartmentCategoryById(int id);
        Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<dynamic> GetByIdAsync(int id);
        Task<int> CreateAsync(Department dep);
        Task<int> UpdateAsync(Department dep);
        Task<PagedResult<dynamic>> GetAllStates();
        Task<PagedResult<dynamic>> GetCitiesByStateId(int StateId);


    }
}
