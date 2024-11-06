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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProjectEntity>()
                .HasOne(up => up.Employee)
                .WithMany(u => u.EmployeeProjects)
                .HasForeignKey(up => up.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProjectEntity>()
                .HasOne(up => up.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Author)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Performer)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.PerformerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
