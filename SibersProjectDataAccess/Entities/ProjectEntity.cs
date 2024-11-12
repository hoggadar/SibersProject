namespace SibersProjectDataAccess.Entities
{
    public class ProjectEntity
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string CustomerCompany { get; set; } = null!;
        public string PerformerCompany { get; set; } = null!;
        public long DirectorId { get; set; }
        public UserEntity? Director { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public int Priority { get; set; }
        public ICollection<UserProjectEntity>? EmployeeProjects { get; set; }
        public ICollection<TaskEntity>? Tasks { get; set; }
        public ICollection<ProjectDocumentEntity>? Documents { get; set; }
    }
}
