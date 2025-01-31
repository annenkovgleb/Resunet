using ResunetDal.Interfaces;
using ResunetDal.Models;

namespace ResunetDal.Implementations
{
    public class DbSessionDAL : IDbSessionDAL
    {
        public async Task Create(SessionModel model)
        {
            string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                values (@DbSessionID, @SessionData, @Created, @LastAccessed, @UserId)";
            await DbHelper.ExecuteAsync(sql, model);
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId 
                from DbSession where DbSessionID = @sessionId";
            var sessions = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId });
            return sessions.FirstOrDefault();
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";
            await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId });
        }

        public async Task Update(Guid dbSessionID, string sessionData)
        {
            string sql = @"update DbSession
                      set SessionData = @SessionData
                      where DbSessionID = @DbSessionID";

            await DbHelper.ExecuteAsync(sql, new { dbSessionID, sessionData });
        }

        public async Task Extend(Guid dbSessionID)
        {
            string sql = @"update DbSession
                    set LastAccessed = @lastAccessed
                    where DbSessionID = @dbSessionID";

            await DbHelper.ExecuteAsync(sql, new { dbSessionID, lastAccessed = DateTime.Now });
        }
    }
}

