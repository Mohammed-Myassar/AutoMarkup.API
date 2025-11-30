using Domain.AccountEntity;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interface;

namespace Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly AutoMarkupDb _context;
        public AccountRepository(AutoMarkupDb context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithProjectsAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(
                u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }
    }
}
