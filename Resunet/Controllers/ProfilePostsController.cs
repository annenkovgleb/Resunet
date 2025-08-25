using Microsoft.AspNetCore.Mvc;
using ResunetBl.Auth;
using Resunet.Data;
using System.Net;
using Resunet.Middleware;
using Resunet.ViewModels;

namespace Resunet.Controllers;

[SiteAuthorize]
public class ProfilePostsController(
    IPost _post,
    ICurrentUser _currentUser) : Controller
{
    [HttpGet]
    [Route("/profile/post/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = await _currentUser.GetCurrentUserId() ?? 0;
        PostViewModel viewmodel = new PostViewModel();

        if (id != 0)
        {
            var postModel = await _post.GetPost(id);
            if (postModel is null || postModel.UserId != userId)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                viewmodel = ViewMapper.PostMapper.MapPostModelToPostViewModel(postModel);
            }
        }

        return View("Edit", viewmodel);
    }
}