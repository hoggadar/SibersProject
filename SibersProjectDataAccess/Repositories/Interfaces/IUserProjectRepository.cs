using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IUserProjectRepository : IRepository<UserProjectEntity>
    {
        Task CreateRange(List<UserEntity> users);
        Task<UserProjectEntity?> GetByUserAndProject(long userId, long projectId);
    }
}
