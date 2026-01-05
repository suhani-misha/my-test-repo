using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using SocialSecurity.Infrastructure.Data;
using System.Linq.Expressions;


namespace SocialSecurity.Application.UnitOfWorks
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext Context;
        internal DbSet<T> dbSet;
        readonly SieveProcessor _sieveprocessor;
        public Repository(AppDbContext context, SieveProcessor sieveProcessor)
        {
            Context = context;
            this.dbSet = context.Set<T>();
            this._sieveprocessor = sieveProcessor;
        }
        public async Task<T> AddAsync(T entity)
        {
            var result = await dbSet.AddAsync(entity);

            return result.Entity;
        }
        public async Task<List<T>> AddRangeAsync(List<T> entity)
        {
            await dbSet.AddRangeAsync(entity);

            return entity;
        }
        public T Add(T entity)
        {
            var result = dbSet.Add(entity);
            return result.Entity;
        }
        public List<T> AddRange(List<T> entity)
        {
            try
            {
                dbSet.AddRange(entity);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return entity;

        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public List<T> UpdateRange(List<T> entity)
        {
            try
            {
                dbSet.UpdateRange(entity);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return entity;

        }


        public T? Get(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<T?> GetAsync(int id)
        {
            var result = await Context.Set<T>().FindAsync(id);
            return result;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }
        public IEnumerable<T> GetAllBySieve(SieveModel sievemodel = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (sievemodel != null)
            {
                query = _sieveprocessor.Apply(sievemodel, query);
            }

            return query.ToList();
        }
        public IEnumerable<T> GetAllNoTracking(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.AsNoTracking().ToList();
        }

        public List<Ttype>? GetSelectedNoTracking<Ttype>(Expression<Func<T, Ttype>> select, Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.AsNoTracking().Select(select).ToList();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            var hasIdProperty = typeof(T).GetProperties().Any(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            if(hasIdProperty)
            {
                query = query.OrderByDescending(x => x);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
                        
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>?> GetLimitAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int limit = 0, int skip = 0)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            query = query.Skip(skip).Take(limit);
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }
        public T? GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public void Remove(int id)
        {
            T? entityToRemove = dbSet?.Find(id);
            if (entityToRemove != null)
                Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public object ExecuteQuery(string query, params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters);
        }

        public IQueryable<T> GetQueryable(
    Expression<Func<T, bool>>? filter = null,
    string? includeProperties = null,
    bool asNoTracking = false)
        {
            IQueryable<T> query = dbSet;

            // Apply filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include navigation properties (comma-separated)
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Optionally disable tracking for performance
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }


        public DbContext GetDbContext()
        {
            return Context;
        }
    }
}
