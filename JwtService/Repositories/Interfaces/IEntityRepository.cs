using JwtService.Commons;
using JwtService.Entities.Interfaces;
using System.Threading.Tasks;

namespace JwtService.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task<Result> Save(T entity);
        Result Delete(T entity);
    }
}
