using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetAllByEmployeeId(long employeeId);
        Task<TaskEntity?> GetByTitle(string title);
    }
}
