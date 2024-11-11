using Microsoft.EntityFrameworkCore;
using SibersProjectDataAccess.Data;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectDataAccess.Repositories.Implementations
{
    public class TaskRepository : Repository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }
        
        public async Task<IEnumerable<TaskEntity>> GetAllByEmployeeId(long employeeId)
        {
            return await _context.Tasks.Where(x => x.PerformerId == employeeId).ToListAsync();
        }

        public async Task<TaskEntity?> GetByTitle(string title)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}
