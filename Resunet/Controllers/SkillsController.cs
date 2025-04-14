using Microsoft.AspNetCore.Mvc;
using Resunet.Middleware;
using Resunet.ViewMapper;
using Resunet.ViewModels;
using ResunetBl.Auth;
using ResunetBl.Profile;
using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace Resunet.Controllers
{
    [SiteAuthorize]
    public class SkillsController(
        IProfile _profile,
        ICurrentUser _currentUser,
        ISkillDAL _skill)
        : Controller
    {
        public async Task<IActionResult> My()
        {
            var p = await _currentUser.GetProfiles();
            var myskills = await _profile.GetProfileSkills(p.FirstOrDefault()?.ProfileId ?? 0);
            return new JsonResult(myskills.Select(m => new SkillViewModel { Name = m.SkillName, Level = m.Level }));
        }

        [HttpPut]
        public async Task<IActionResult> Add([FromBody] SkillViewModel skill)
        {
            var p = await _currentUser.GetProfiles();
            ProfileSkillModel profileSkillModel = SkillMapper.MapSkillViewModelToProfileSkillModel(skill);
            profileSkillModel.ProfileId = p.FirstOrDefault()?.ProfileId ?? 0;
            await _profile.AddProfileSkill(profileSkillModel);
            return Ok();
        }

        [Route("skills/search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            var skills = await _skill.Search(5, search);
            return new JsonResult(skills?.Select(m => m.SkillName).ToList());
        }
    }
}