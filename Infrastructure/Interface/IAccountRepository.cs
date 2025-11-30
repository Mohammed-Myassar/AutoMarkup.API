using Domain.AccountEntity;

namespace Infrastructure.Interface
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);
        Task<User?> GetByIdWithProjectsAsync(Guid userId);
    }
}
