using Ekart.Core.Entites;

namespace Ekart.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<bool> Complete();
    }
}
