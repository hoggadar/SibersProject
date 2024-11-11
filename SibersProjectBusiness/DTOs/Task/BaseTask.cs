namespace SibersProjectBusiness.DTOs.Task
{
    public class BaseTask
    {
        public string Title { get; set; } = null!;
        public long AuthorId { get; set; }
        public long PerformerId { get; set; }
        public long ProjectId { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; } = null!;
    }
}
