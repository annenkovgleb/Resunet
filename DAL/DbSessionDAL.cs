using Dapper;
using Npgsql;
using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        public async Task<SessionModel?> GetSession(Guid sessionId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                string sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccess, UserId from DbSession where DbSessionId)";

                var sessions = await connection.QueryAsync<SessionModel>(sql);
                return sessions.FirstOrDefault();
            }
        }

        public async Task<int> UpdateSession(SessionModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                string sql = @"update DbSession
                        set SessionData = @SessionData, LastAccess = @LastAccess, UserId = @UserId
                        where DbSessionID = @DbSessionID
                ";

                return await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task<int> CreateSession(SessionModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                string sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccess)
                        values (@DbSessionId, @SessionData, @Created, @LastAccess)";

                return await connection.ExecuteAsync(sql, model);
            }
        }
    }
}
