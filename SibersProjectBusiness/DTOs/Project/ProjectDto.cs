namespace SibersProjectBusiness.DTOs.Project
{
    public class ProjectDto : BaseProject
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
    }
}
