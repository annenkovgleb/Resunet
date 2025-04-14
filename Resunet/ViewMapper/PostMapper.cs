using Resunet.ViewModels;
using ResunetDAL.Models;

namespace Resunet.ViewMapper
{
    public static  class PostMapper
    {
        public static PostModel MapPostViewModelToPostModel(PostViewModel viewModel)
        {
            return new PostModel()
            {
                PostId = viewModel.PostId,
                Title = viewModel.Title ?? "",
                Intro = viewModel.Intro ?? "",
                Status = viewModel.Status
            };
        }

        public static PostViewModel MapPostModelToPostViewModel(PostModel model)
        {
            return new PostViewModel()
            {
                PostId = model.PostId,
                Title = model.Title,
                Intro = model.Intro,
                Status = model.Status
            };
        }

        public static IEnumerable<PostContentModel>MapPostitemViewModelToPostItemModel(IEnumerable<PostContentItemViewModel> items)
        {
            foreach(PostContentItemViewModel model in items)
            {
                yield return new PostContentModel()
                {
                    PostContentId = model.PostContentId,
                    ContentItemType = (int)model.ContentItemType,
                    Value = model.Value ?? "",
                    // PostId = postid
                };
            }
        }
    }
}
