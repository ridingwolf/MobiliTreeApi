using MobiliTree.Domain.Models;

namespace MobiliTree.Domain.Repositories
{
    public interface ISessionsRepository
    {
        void AddSession(Session session);
        List<Session> GetSessions(string parkingFacilityId);
    }
}