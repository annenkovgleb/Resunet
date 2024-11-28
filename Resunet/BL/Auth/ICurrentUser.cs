using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
        Task<IEnumerable<ProfileModel>> GetProfiles();
    }
}

