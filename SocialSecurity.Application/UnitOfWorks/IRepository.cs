using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using System.Linq.Expressions;

namespace SocialSecurity.Application.UnitOfWorks
{
    public interface IRepository<T> where T : class
    {
        T? Get(int id);

        Task<T?> GetAsync(int id);

        IEnumerable<T>? GetAll(
           Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string? includeProperties = null
           );
        IEnumerable<T>? GetAllBySieve(
           SieveModel sievemodel = null,
           string? includeProperties = null
           );
        IEnumerable<T>? GetAllNoTracking(
          Expression<Func<T, bool>>? filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          string? includeProperties = null
          );
        List<Ttype>? GetSelectedNoTracking<Ttype>(
         Expression<Func<T, Ttype>> select,
         Expression<Func<T, bool>>? filter = null,
         string? includeProperties = null
         );
        Task<IEnumerable<T>?> GetAllAsync(
           Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string? includeProperties = null
           );
        Task<IEnumerable<T>?> GetLimitAsync(
          Expression<Func<T, bool>>? filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          string? includeProperties = null, int records = 0, int skip = 0
          );
        T? GetFirstOrDefault(
           Expression<Func<T, bool>>? filter = null,
           string? includeProperties = null
           );

        Task<T?> GetFirstOrDefaultAsync(
           Expression<Func<T, bool>>? filter = null,
           string? includeProperties = null
           );
        T Add(T entity);
        Task<T> AddAsync(T entity);
        List<T> AddRange(List<T> entity);
        Task<List<T>> AddRangeAsync(List<T> entity);
        void Update(T entity);
        List<T> UpdateRange(List<T> entity);
      
        object ExecuteQuery(string query, params object[] parameters);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IQueryable<T> GetQueryable(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null,
            bool asNoTracking = false
        );
        DbContext GetDbContext();
        

    }
}
