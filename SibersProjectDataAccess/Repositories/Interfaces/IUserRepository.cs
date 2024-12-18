﻿using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IEnumerable<UserEntity>> GetAllByProjectId(long projectId);
        Task<UserEntity?> GetByEmail(string email);
    }
}
