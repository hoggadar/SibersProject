using SibersProjectDataAccess.Data;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectDataAccess.Repositories.Implementations
{
    public class UserProjectRepository : Repository<UserProjectEntity>, IUserProjectRepository
    {
        public UserProjectRepository(AppDbContext context) : base(context) { }

        public async Task CreateRange(List<UserEntity> users)
        {
            await _context.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
