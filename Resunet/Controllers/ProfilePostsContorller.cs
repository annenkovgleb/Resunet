using Microsoft.AspNetCore.Mvc;
using ResunetBl.Middleware;
using ResunetBl.Auth;
using Resunet.Data;
using System.Net;
using Resunet.ViewModels;

namespace Resunet.Controllers
{
    [SiteAuthorize()]
    public class ProfilePostsContorller : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IPost post;

        public ProfilePostsContorller(ICurrentUser currentUser, IPost post)
        {
            this.currentUser = currentUser;
            this.post = post;
        }

        [HttpGet]
        [Route("/profile/post/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await currentUser.GetCurrentUserId() ?? 0;
            PostViewModel viewmodel = new PostViewModel();

            if (id != 0)
            {
                var postModel = await post.GetPost(id);
                if (postModel == null || postModel.UserId != userId)
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
}
