using JwtService.Commons;
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

        public virtual async Task<Result> Save(T entity)
        {
            return await Result.DoAndReturnResultAsync(async () =>
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            });
        }

        public virtual async Task<Result> Delete(T entity)
        {
            return await Result.DoAndReturnResultAsync(async () =>
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
            });
        }

        public virtual async Task<Result<T>> FindById(long id)
        {
            var result = new Result<T>();
            var value = await _dbContext.FindAsync<T>(id);
            if (value == null)
            {
                result.Add("Record not found.");
                return result;
            }

            return result.Ok(value);
        }
    }
}
