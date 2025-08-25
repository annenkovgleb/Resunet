using Microsoft.AspNetCore.Mvc;
using ResunetBl.Resume;

namespace Resunet.Controllers;

public class ResumeController(IResume _resume) : Controller
{
    [Route("/resume/{profileid}")]
    public async Task<IActionResult> Index(int profileid)
    {
        var model = await _resume.Get(profileid);
        return View(model);
    }
}