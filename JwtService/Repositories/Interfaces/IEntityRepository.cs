﻿using JwtService.Commons;
using JwtService.Entities.Interfaces;
using System.Threading.Tasks;

namespace JwtService.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task<Result> Save(T entity);
        Task<Result> Delete(T entity);
        Task<Result> Delete(long id);
        Task<Result<T>> FindById(long id);
    }
}
