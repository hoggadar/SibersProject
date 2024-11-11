using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepo, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userRepo.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetAllByProjectId(long projectId)
        {
            var users = await _userRepo.GetAllByProjectId(projectId);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetById(long id)
        {
            var user = await _userRepo.GetById(id);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<UserDto?> GetByEmail(string email)
        {
            var user = await _userRepo.GetByEmail(email);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<UserDto> Create(CreateUserDto dto)
        {
            if (!Enum.TryParse<RoleEnum>(dto.Role, true, out var _))
            {
                throw new ArgumentException($"Invalid role: {dto.Role}");
            }
            var newUser = _mapper.Map<UserEntity>(dto);
            var createdUser = await _userRepo.Create(newUser);
            return _mapper.Map<UserDto>(createdUser);
        }

        public async Task<UserDto?> Update(long id, UpdateUserDto dto)
        {
            var existingUser = await _userRepo.GetById(id);
            if (existingUser == null) return null;
            if (!Enum.TryParse<RoleEnum>(dto.Role, true, out var _))
            {
                throw new ArgumentException($"Invalid role: {dto.Role}");
            }
            var updatedUser = _mapper.Map(dto, existingUser);
            var result = await _userRepo.Update(updatedUser);
            return _mapper.Map<UserDto?>(result);
        }

        public async Task<UserDto?> Delete(long id)
        {
            var user = await _userRepo.GetById(id);
            if (user == null) return null;
            var deletedUser = await _userRepo.Delete(user);
            return _mapper.Map<UserDto?>(deletedUser);
        }
    }
}
