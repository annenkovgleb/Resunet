using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace Resunet.Data
{
    public class Post : IPost
    {
        private readonly ResunetDAL.Interfaces.IPost _post;

        public Post(ResunetDAL.Interfaces.IPost post)
        {
            this._post = post;
        }

        public async Task<PostModel> GetPost(int postId)
        {
            return await this._post.GetPost(postId);
        }

        public async Task<int> AddOrUpdate(PostModel model)
        {
            model.Modified = DateTime.Now;

            if (model.PostId == null)
            {
                model.Created = DateTime.Now;
                return await this._post.CreatePost(model);
            }
            else
            {
                await this._post.UpdatePost(model);
                return model.PostId ?? 0;
            }

        }

        public async Task<List<PostContentModel>> GetPostItems(int postId)
        {
            var result = await this._post.GetPostContent(postId);
            return result.ToList();
        }

        public async Task AddOrUpdateContentItems(IEnumerable<PostContentModel> items)
        {
            foreach (PostContentModel model in items)
            {
                if (model.PostContentId == null)
                {
                    model.PostContentId = await this._post.CreatePostContent(model);
                }

                else
                {
                    await this._post.UpdatePostContent(model);
                }
            }
        }
    }
}
