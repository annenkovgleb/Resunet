using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public class Skill : ISkill
    {
        private readonly ISkillDAL skillDAL;

        public Skill(ISkillDAL skillDAL)
        {
            this.skillDAL = skillDAL;
        }

        public async Task<IEnumerable<SkillModel>> Search(int top, string skillname)
        {
            return await skillDAL.Search(top, skillname);
        }
    }
}
