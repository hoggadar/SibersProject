namespace SibersProjectBusiness.DTOs.Project
{
    public class UpdateProjectDto
    {
        public string Title { get; set; } = null!;
        public string CustomerCompany { get; set; } = null!;
        public string PerformerCompany { get; set; } = null!;
        public long DirectorId { get; set; }
        public int Priority { get; set; }
    }
}
