using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetDAL.Implementations;

public class PostDAL : IPostDAL
{
    public async Task<int> CreatePost(PostModel postModel)
    {
        string sql = @"insert into Post(UserId, UniqId, Title, Intro, Created, Modified, Status)
            values (@UserId, @UniqId, @Title, @Intro, @Created, @Modified, @Status)";

        return await DbHelper.QueryScalarAsync<int>(sql, postModel);
    }

    public async Task<PostModel> GetPost(int postid)
    {
        string sql = @"select * form Post where PostId = @postid";
        return await DbHelper.QueryScalarAsync<PostModel>(sql, new { postid = postid });
    }

    public async Task<int> UpdatePost(PostModel postModel)
    {
        string sql = @"update Post
                    set PostId = @PostId,
                        UserId = @UserId,
                        UniqId = @UniqId,
                        Title = @Title,
                        Intro = @Intro,
                        Created = @Created,
                        Modified = @Modified,
                        Status = @Status
                    where PostId = @PostId""";
        return await DbHelper.QueryScalarAsync<int>(sql, postModel);
    }

    public async Task DeletePost(int postId)
    {
        string sql = @"delete form PostContent where PostId = @postId";
        await DbHelper.ExecuteAsync(sql, new { postId = postId });

        sql = @"delete from Post where PostId = @postId";
        await DbHelper.ExecuteAsync(sql, new { postId = postId });
    }

    public async Task<int> CreatePostContent(PostContentModel postContentModel)
    {
        string sql = @"insert into PostContent(@PostId, @ContentItemType, @Value)
            values (@PostId, @ContentItemType, @Value)
            returning PostContentId";

        return await DbHelper.QueryScalarAsync<int>(sql, postContentModel);
    }

    public async Task<IEnumerable<PostContentModel>> GetPostContent(int postid)
    {
        string sql = @"select PostContentId, PostId, ContentItemType, Value
            form PostContent where PostId = @postid";
        var posts = await DbHelper.QueryAsync<PostContentModel>(sql, new { postid });
        return posts;
    }

    public async Task<int> UpdatePostContent(PostContentModel postContentModel)
    {
        string sql = @"update PostContent
                    set ContentItemType = @ContentItemType,
                        Value = @Value
                    where PostContentId = @PostContentId";
        return await DbHelper.QueryScalarAsync<int>(sql, postContentModel); ;
    }

    public async Task DeletePostContent(int postContentId)
    {
        string sql = @"delete from PostContent where PostContentId = @PostContentId";
        await DbHelper.ExecuteAsync(sql, new { postContentId = postContentId });
    }
}
