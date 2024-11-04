using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAll();
    }
}
