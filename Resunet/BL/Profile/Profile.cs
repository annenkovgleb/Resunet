using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public class Profile : IProfile
    {
        public Profile()
        {
        }

        public Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
