using System;
using Resunet.DAL;
using Resunet.DAL.Models;
using Microsoft.AspNetCore.Http;
using Resunet.BL.Auth;

namespace Resunet.BL.Auth
{
    public class DbSession : IDbSession
    {
        public Task<SessionModel> GetSession()
        {
            throw new NotImplementedException();
            //    Guid sessionId;
            //    // var cookie = HttpContextAccessor.
        }

        public Task<int?> GetUserId()
        {
            //    var data = await this.GetSession();
            //    return data.UserId;
            throw new NotImplementedException();
        }

        public async Task<int> IsLoggedIn()
        {
            var data = await this.GetSession();
            return data.UserId;
        }

        public Task<int> SetUserId(int userId)
        {
            //    var data = await this.GetSession();
            //    data.UserId = userId;
            //    data.DbSessionId = Guid.NewGuid().ToString();
            //    CreateSessionCookie(data.DbSessionId);
            //    return await DbSessionDAL.
            throw new NotImplementedException();
        }
    }
}
