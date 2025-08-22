using ResunetDAL.Models;

namespace ResunetDAL.Interfaces;

public interface IDbSession
{
    Task<SessionModel?> Get(Guid sessionId);

    Task Update(Guid dbSessionID, string sessionData);

    Task Create(SessionModel model);

    Task Lock(Guid sessionId);

    Task Extend(Guid dbSessionID);
}
