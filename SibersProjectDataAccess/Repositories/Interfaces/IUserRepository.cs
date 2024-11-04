using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmail(string email);
    }
}
