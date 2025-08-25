using ResunetDAL.Models;

namespace Resunet.Data;

public class Post(ResunetDAL.Interfaces.IPostDAL _post) : IPost
{
    public async Task<PostModel> GetPost(int postId)
        => await _post.GetPost(postId);

    public async Task<int> AddOrUpdate(PostModel model)
    {
        model.Modified = DateTime.Now;

        if (model.PostId is null)
        {
            model.Created = DateTime.Now;
            return await _post.CreatePost(model);
        }
        else
        {
            await _post.UpdatePost(model);
            return model.PostId ?? 0;
        }
    }

    public async Task<List<PostContentModel>> GetPostItems(int postId)
    {
        var result = await _post.GetPostContent(postId);
        return result.ToList();
    }

    public async Task AddOrUpdateContentItems(IEnumerable<PostContentModel> items)
    {
        foreach (PostContentModel model in items)
        {
            if (model.PostContentId is null)
            {
                model.PostContentId = await _post.CreatePostContent(model);
            }
            else
            {
                await _post.UpdatePostContent(model);
            }
        }
    }
}