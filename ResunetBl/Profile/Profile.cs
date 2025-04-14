using ResunetDAL.Models;

namespace ResunetBl.Profile
{
    public class Profile(ResunetDAL.Interfaces.IProfile profile, ResunetDAL.Interfaces.ISkill _skill) : IProfile
    {
        public async Task AddOrUpdate(ProfileModel model)
        {
            if (model.ProfileId == null)
            {
                model.ProfileId = await profile.Add(model);
            }
            else
            {
                await profile.Update(model);
            }
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await profile.GetByUserId(userId);
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            return await _skill.GetProfileSkills(profileId);
        }

        public async Task AddProfileSkill(ProfileSkillModel model)
        {
            var skill = await _skill.Get(model.SkillName);
            if (skill is not null || skill.SkillId is null)
            {
                model.SkillId = await _skill.Create(model.SkillName);
            }
            else
            {
                model.SkillId = skill.SkillId ?? 0;
            }

            await _skill.AddProfileSkill(model);
        }
    }
}