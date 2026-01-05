
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sieve.Services;
using SocialSecurity.Domain.Models;
using SocialSecurity.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;



namespace SocialSecurity.Application.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;
        private IDbContextTransaction transaction = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private bool disposed = false;

        public UnitOfWork(AppDbContext dbcontext, IHttpContextAccessor httpContextAccessor, SieveProcessor sieveProcessor)
        {
            _context = dbcontext;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task BeginTrans()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task Commit()
        {
            await transaction.CommitAsync();
        }
        public async Task RollBackTrans()
        {
            transaction.Rollback();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void ClearTracker()
        {
            _context.ChangeTracker.Clear();
        }

        public void DetachEntity(object entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                return;
            }

            // Set the state to Detached
            entry.State = EntityState.Detached;
        }

    }
}
