using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Resunet.Models;
using ResunetBl.Resume;

namespace Resunet.Controllers;

public class HomeController(IResume _resume) : Controller
{
    public async Task<IActionResult> Index()
    {
        var latestResumes = await _resume.Search(4);
        return View(latestResumes);
    }

    public IActionResult Privacy()
        => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}