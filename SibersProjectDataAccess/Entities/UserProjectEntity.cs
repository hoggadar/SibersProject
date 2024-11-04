namespace SibersProjectDataAccess.Entities
{
    public class UserProjectEntity
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public UserEntity? Employee { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity? Project { get; set; }
    }
}
