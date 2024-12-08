using ResunetDal.Models;

namespace ResunetDal.Interfaces
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);

        Task Update(Guid dbSessionID, string sessionData);

        Task Create(SessionModel model);

        Task Lock(Guid sessionId);

        Task Extend(Guid dbSessionID);
    }
}
