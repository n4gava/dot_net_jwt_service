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
                if (entity.ID == 0)
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

        public async Task<Result> Delete(long id)
        {
            var entity = (await FindById(id)).Value;

            if (entity == null)
                return Result.Ok();

            return await Delete(entity);
        }

        public virtual async Task<Result<T>> FindById(long id)
        {
            var result = new Result<T>();
            var value = await _dbContext.FindAsync<T>(id);
            if (value == null)
                return result.Add("Record not found.");

            return result.Ok(value);
        }
    }
}
