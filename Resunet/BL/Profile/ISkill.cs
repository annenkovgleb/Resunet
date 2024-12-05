using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public interface ISkill
    {
        Task<IEnumerable<SkillModel>> Search(int top, string skillname);
    }
}
