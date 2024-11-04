using SibersProjectBusiness.Interfaces;
using SibersProjectDataAccess.Entities;
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
    }
}
