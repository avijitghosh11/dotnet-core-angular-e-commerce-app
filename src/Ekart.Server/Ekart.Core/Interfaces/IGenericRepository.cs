using Ekart.Core.Entites;
using Ekart.Core.Specifications.Interface;

namespace Ekart.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity 
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<TResult?> GetWithSpec<TResult>(ISpecification<T, TResult> spec);
        Task<IReadOnlyList<TResult>> GetAsync<TResult>(ISpecification<T, TResult> spec);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        bool IsExists(int id);
        Task<bool> SaveAllAsync();
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
