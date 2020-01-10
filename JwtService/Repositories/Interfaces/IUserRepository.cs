using JwtService.Commons;
using JwtService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Repositories.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        Task<Result<User>> FindByEmail(string email);
        
    }
}
