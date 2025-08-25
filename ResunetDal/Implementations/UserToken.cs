using ResunetDAL.Interfaces;

namespace ResunetDAL.Implementations;

public class UserToken : IUserToken
{
    public async Task<Guid> Create(int userid)
    {
        Guid tokenid = Guid.NewGuid();
        string sql = @"insert into UserToken (UserTokenId, UserId, Created)
                values (@tokenid, @userid, NOW())";
        // The NOW() function returns the current date and time with the time zone of the database server
        // на сервере бд и сервере приложения - могут быть разное время, могут быть коллизии

        await DbHelper.ExecuteAsync(sql, new { userid, tokenid });
        return tokenid;
    }

    public async Task<int?> Get(Guid tokenid)
    {
        string sql = @"select UserId from UserToken where UserTokenId = @tokenid";
        return await DbHelper.QueryScalarAsync<int?>(sql, new { tokenid });
    }
}
