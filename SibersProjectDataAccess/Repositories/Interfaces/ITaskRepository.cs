using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<TaskEntity?> GetByTitle(string title);
    }
}
