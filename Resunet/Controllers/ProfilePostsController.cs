using Microsoft.AspNetCore.Mvc;
using Resunet.Data;
using Resunet.Middleware;
using Resunet.Service;
using Resunet.ViewModels;
using ResunetBl.Auth;
using System.Net;

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

    [HttpGet]
    [Route("/profile/postdata/{id}")]
    public async Task<IActionResult> PostData(int id)
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

        return new JsonResult(viewmodel); // json для компонентов
    }

    [HttpPut]
    [Route("/profile/post")]
    public async Task<IActionResult> EditSave([FromBody] PostViewModel model)
    {
        return new ContentResult();
    }

    [HttpGet]
    [Route("/profile/post/image")]
    public async Task<IActionResult> UploadImage()
    {
        var userid = await _currentUser.GetCurrentUserId();
        WebFile webfile = new WebFile();
        string fileName = webfile.GetWebFileName(userid + "-" + Request.Form.Files[0].FileName, "postimages");
        await webfile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), fileName, 800, 600);

        return new JsonResult(new
        {
            Filename = fileName
        });
    }
}