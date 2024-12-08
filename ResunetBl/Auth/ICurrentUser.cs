using ResunetDal.Models;

namespace ResunetBl.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
        Task<IEnumerable<ProfileModel>> GetProfiles();
    }
}

