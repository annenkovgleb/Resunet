using ResunetDAL.Models;

namespace Resunet.Data;

public interface IPost
{
    Task<PostModel> GetPost(int postId);

    Task<int> AddOrUpdate(PostModel model);

    Task<List<PostContentModel>> GetPostItems(int postId);

    Task AddOrUpdateContentItems(IEnumerable<PostContentModel> items);
}