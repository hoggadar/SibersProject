using Microsoft.EntityFrameworkCore;
using SibersProjectDataAccess.Data;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectDataAccess.Repositories.Implementations
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<UserEntity>> GetUsersByProjectId(long projectId)
        {
            var users = await _context.UserProjects
            .Where(up => up.ProjectId == projectId)
            .Include(up => up.Employee)
            .Select(up => new UserEntity
            {
                Id = up.Employee.Id,
                FirstName = up.Employee.FirstName,
                LastName = up.Employee.LastName,
                Patronymic = up.Employee.Patronymic,
                Email = up.Employee.Email,
                Role = up.Employee.Role
            })
            .ToListAsync();
            return users;
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
