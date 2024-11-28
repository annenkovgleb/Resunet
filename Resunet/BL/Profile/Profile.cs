using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;

        public Profile(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }

        public async Task AddOrUpdate(ProfileModel model)
        {
            if (model.ProfileId == null)
                await profileDAL.Add(model);
            else
                await profileDAL.Update(model);
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await profileDAL.Get(userId);
        }
    }
}
