using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Ekart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ekart.Infrastructure.Repository
{
    public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T obj)
        {
            context.Set<T>().Add(obj);
        }

        public void Delete(T obj)
        {
            context.Set<T>().Remove(obj);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> GetAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<TResult?> GetWithSpec<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public bool IsExists(int id)
        {
            return context.Set<T>().Any(x => x.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(T obj)
        {
            context.Set<T>().Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(),spec);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T,TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T,TResult>(context.Set<T>().AsQueryable(), spec);
        }
    }
}
