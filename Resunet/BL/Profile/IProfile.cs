using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);
    }
}
