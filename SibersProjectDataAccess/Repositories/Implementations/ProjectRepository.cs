using Microsoft.EntityFrameworkCore;
using SibersProjectDataAccess.Data;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectDataAccess.Repositories.Implementations
{
    public class ProjectRepository : Repository<ProjectEntity>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ProjectEntity>> GetAllByDirectorId(long directorId)
        {
            return await _context.Projects.Where(x => x.DirectorId == directorId).ToListAsync();
        }

        public async Task<ProjectEntity?> GetByTitle(string title)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}
