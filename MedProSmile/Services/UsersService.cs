using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
            => _repository.GetAllPagedAsync(pageNumber, pageSize);

        public Task<dynamic> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<int> CreateAsync(Users users)
            => _repository.CreateAsync(users);

        public Task<int> UpdateAsync(UsersUpdate usersUpdate)
            => _repository.UpdateAsync(usersUpdate);

        public Task<int> DeleteAsync(UsersDelete usersDelete)
            => _repository.DeleteAsync(usersDelete);
    }
}
