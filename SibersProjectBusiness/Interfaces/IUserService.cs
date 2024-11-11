using SibersProjectBusiness.DTOs.User;

namespace SibersProjectBusiness.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<IEnumerable<UserDto>> GetAllByProjectId(long projectId);
        Task<UserDto?> GetById(long id);
        Task<UserDto?> GetByEmail(string email);
        Task<UserDto> Create(CreateUserDto dto);
        Task<UserDto?> Update(long id, UpdateUserDto dto);
        Task<UserDto?> Delete(long id);
    }
}
