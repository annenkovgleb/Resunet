using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Resunet.Models;
using ResunetBl.Auth;
using ResunetBl.Resume;

namespace Resunet.Controllers;

public class HomeController(
    IResume _resume,
    ILogger<HomeController> logger,
    ICurrentUser currentUser)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<IActionResult> Index()
    {
        var latestResumes = await _resume.Search(4);
        return View(latestResumes);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}