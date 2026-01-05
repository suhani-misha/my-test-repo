
using SocialSecurity.Domain.Models;

namespace SocialSecurity.Application.UnitOfWorks
{
    public interface IUnitOfWork
    {

        Task SaveAsync();
        void Save();
        void ClearTracker();
        Task BeginTrans();
        Task Commit();
        Task RollBackTrans();
        void DetachEntity(object dbOldData);
    }
}
