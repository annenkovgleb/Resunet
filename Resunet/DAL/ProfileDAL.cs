using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(ProfileModel profile)
        {
            string sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage)
                        values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage) returning ProfileId";
            var result = await DbHelper.QueryAsync<int>(sql, profile);
            return result.First();
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@"
                        select ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                        from Profile
                        where UserId = @id", new { id = userId });
        }

        public async Task Update(ProfileModel profile)
        {

            string sql = @"Update Profile
                        set ProfileName = @ProfileName,
                            FirstName = @FirstName, 
                            LastName = @LastName, 
                            ProfileImage = @ProfileImage
                        where ProfileId = @ProfileId";
            var result = await DbHelper.QueryAsync<int>(sql, profile);
        }
    }
}
