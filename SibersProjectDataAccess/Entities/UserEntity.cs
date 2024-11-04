using SibersProjectDataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace SibersProjectDataAccess.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public RoleEnum Role { get; set; }
        public ICollection<UserProjectEntity>? EmployeeProjects { get; set; }
        public ICollection<TaskEntity>? Tasks { get; set; }
        //public ICollection<TaskEntity>? AssignedTasks { get; set; }
    }
}
