using SibersProjectBusiness.DTOs.User;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity?> GetById(long id);
        Task<UserEntity?> GetByEmail(string email);
        Task<UserEntity> Create(CreateUserDto dto);
        Task<UserEntity> Update(UserEntity entity);
        Task<UserEntity?> Delete(long id);
    }
}
