using System;
namespace Resunet.BL.Auth
{
    public interface ICurrentUser
    {
        public Task<bool> IsLoggedIn();
    }
}

