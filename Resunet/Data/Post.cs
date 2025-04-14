using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace Resunet.Data
{
    public class Post : IPost
    {
        private readonly IPostDAL postDAL;

        public Post(IPostDAL postDAL)
        {
            this.postDAL = postDAL;
        }

        public async Task<PostModel> GetPost(int postId)
        {
            return await this.postDAL.GetPost(postId);
        }

        public async Task<int> AddOrUpdate(PostModel model)
        {
            model.Modified = DateTime.Now;

            if (model.PostId == null)
            {
                model.Created = DateTime.Now;
                return await this.postDAL.CreatePost(model);
            }
            else
            {
                await this.postDAL.UpdatePost(model);
                return model.PostId ?? 0;
            }

        }

        public async Task<List<PostContentModel>> GetPostItems(int postId)
        {
            var result = await this.postDAL.GetPostContent(postId);
            return result.ToList();
        }

        public async Task AddOrUpdateContentItems(IEnumerable<PostContentModel> items)
        {
            foreach (PostContentModel model in items)
            {
                if (model.PostContentId == null)
                {
                    model.PostContentId = await this.postDAL.CreatePostContent(model);
                }

                else
                {
                    await this.postDAL.UpdatePostContent(model);
                }
            }
        }
    }
}
