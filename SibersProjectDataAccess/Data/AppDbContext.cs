using Microsoft.EntityFrameworkCore;
using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<UserProjectEntity> UserProjects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
