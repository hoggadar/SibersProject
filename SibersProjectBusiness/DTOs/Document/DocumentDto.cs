namespace SibersProjectBusiness.DTOs.Document
{
    public class DocumentDto
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
    }
}
