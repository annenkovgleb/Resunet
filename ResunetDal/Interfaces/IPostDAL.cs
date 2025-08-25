using ResunetDAL.Models;

namespace ResunetDAL.Interfaces;

public interface IPostDAL
{
    Task<int> CreatePost(PostModel model);

    Task<PostModel> GetPost(int postid);

    Task<int> UpdatePost(PostModel post);

    Task DeletePost(int postid);

    Task<int> CreatePostContent(PostContentModel model);

    Task<IEnumerable<PostContentModel>> GetPostContent(int postid);

    Task<int> UpdatePostContent(PostContentModel model);

    Task DeletePostContent(int post);
}
