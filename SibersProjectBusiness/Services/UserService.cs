using SibersProjectBusiness.DTOs.User;
using SibersProjectBusiness.Interfaces;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Enums;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectBusiness.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _userRepo.GetAll();
        }

        public async Task<UserEntity?> GetById(long id)
        {
            return await _userRepo.GetById(id);
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await _userRepo.GetByEmail(email);
        }

        public async Task<UserEntity> Create(CreateUserDto dto)
        {
            if (!Enum.TryParse<RoleEnum>(dto.Role, true, out var role))
            {
                throw new ArgumentException($"Invalid role: {dto.Role}");
            }
            var newUser = new UserEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Patronymic = dto.Patronymic,
                Email = dto.Email,
                Password = dto.Password,
                Role = role
            };
            return await _userRepo.Create(newUser);
        }

        public async Task<UserEntity> Update(UserEntity entity)
        {
            return await _userRepo.Update(entity);
        }

        public async Task<UserEntity?> Delete(long id)
        {
            var user = await _userRepo.GetById(id);
            if (user == null) return null;
            return await _userRepo.Delete(user);
        }
    }
}
