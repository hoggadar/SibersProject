using SibersProjectDataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SibersProjectDataAccess.Entities
{
    public class TaskEntity
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public long AuthorId { get; set; }
        public UserEntity? Author { get; set; }
        public long PerformerId { get; set; }
        public UserEntity? Performer { get; set; }
        public StatusEnum Status { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity? Project { get; set; }
        public int Priority { get; set; }
    }
}
