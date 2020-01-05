﻿using JwtService.Commons;
using JwtService.Entities.Interfaces;
using JwtService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JwtService.Repositories.Bases
{
    public class BaseRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        DbContext _dbContext;
        public BaseRepository(DbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Save(T entity)
        {
            return await Result.DoAndReturnResultAsync(async () =>
            {
                await _dbContext.AddAsync(entity);
            });
        }

        public Result Delete(T entity)
        {
            return Result.DoAndReturnResult(() =>
            {
                _dbContext.Remove(entity);
            });
        }
    }
}