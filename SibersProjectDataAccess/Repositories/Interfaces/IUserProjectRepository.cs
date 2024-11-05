using SibersProjectDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IUserProjectRepository : IRepository<UserProjectEntity>
    {
        Task CreateRange(List<UserEntity> users);
    }
}
