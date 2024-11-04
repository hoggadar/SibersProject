using Microsoft.EntityFrameworkCore;
using SibersProjectDataAccess.Data;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectDataAccess.Repositories.Implementations
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
