namespace SibersProjectDataAccess.Entities
{
    public class ProjectDocumentEntity
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity? Project { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
    }
}
