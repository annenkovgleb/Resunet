using Microsoft.AspNetCore.Mvc;
using Resunet.Middleware;
using Resunet.Models;
using Resunet.Service;
using Resunet.ViewModels;
using ResunetBl.Auth;
using ResunetBL.Data;
using ResunetDAL.Models;
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
    public async Task<IActionResult> EditSave([FromBody] PostViewModel postView)
    {
        var userid = await _currentUser.GetCurrentUserId() ?? 0;

        // проверка, обновляем чужого пользователя?
        if (ModelState.IsValid && postView.PostId is not null)
        {
            PostModel dbModel = await _post.GetPost(postView.PostId ?? 0);

            if (dbModel.UserId != userid)
            {
                ModelState.TryAddModelError("", "hack");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new ErrorViewModel());
            }
        }

        // проверка контента
        if (postView.PostId is null && postView.ContentItems.Any(m => m.ContentId is not null))
        {
            ModelState.TryAddModelError("Title", "кто-то передает id контента существующих статей");
        }

        if (postView.PostId is not null)
        {
            var existingContentIds = (await _post.GetPostItems(postView.PostId ?? 0))
                .Where(m => m.PostContentId is not null)
                .ToDictionary(m => m.PostContentId ?? 0, m => m.PostId);

            if (postView.ContentItems.Any(m => m.ContentId is not null
                && !existingContentIds.ContainsKey(m.ContentId ?? 0)))
            {
                ModelState.TryAddModelError("Title", "все переданные статьи должны быть существующими или нулевыми");
                return new JsonResult(new ErrorsViewModel(ModelState));
            }
        }

        if (!ModelState.IsValid)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult(new ErrorsViewModel(ModelState));
        }

        // сохранение
        var newModel = ViewMapper.PostMapper.MapPostViewModelToPostModel(postView);
        newModel.UserId = userid;
        int postid = await _post.AddOrUpdate(newModel);
        await _post.AddOrUpdateContentItems(ViewMapper.PostMapper.MapPostItemViewModelToPostItemModel(postView.ContentItems));

        return new JsonResult(new
        {
            id = postid
        });
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