using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetBl.Profile
{
    public class Profile(IProfileDAL _profileDAL, ISkillDAL _skillDAL) : IProfile
    {
        public async Task AddOrUpdate(ProfileModel model)
        {
            if (model.ProfileId == null)
            {
                model.ProfileId = await _profileDAL.Add(model);
            }
            else
            {
                await _profileDAL.Update(model);
            }
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await _profileDAL.GetByUserId(userId);
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            return await _skillDAL.GetProfileSkills(profileId);
        }

        public async Task AddProfileSkill(ProfileSkillModel model)
        {
            var skill = await _skillDAL.Get(model.SkillName);
            if (skill is not null || skill.SkillId is null)
            {
                model.SkillId = await _skillDAL.Create(model.SkillName);
            }
            else
            {
                model.SkillId = skill.SkillId ?? 0;
            }

            await _skillDAL.AddProfileSkill(model);
        }
    }
}