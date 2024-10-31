using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public interface IDbSession
    {
        Task<SessionModel> GetSession();

        Task<int> SetUserId(int userId);

        Task<int?> GetUserId();
        
        Task<int> IsLoggedIn();
    }
}
