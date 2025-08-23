using Resunet.ViewModels;
using ResunetDAL.Models;

namespace Resunet.ViewMapper
{
    public class SkillMapper
    {
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel()
            {
                SkillName = model.Name,
                Level = model.Level,
                ProfileId = model.ProfileId
            };
        }
    }
}