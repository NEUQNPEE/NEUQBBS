using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.DAL.Interfaces;
using WebApiDemo.Entities.EPost;
using WebApiDemo.BLL.Result;
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.BLL;

/// <inheritdoc />
/// <inheritdoc/>
public class PostBll(IPostDalFactory postDalFactory) : IPostBll
{
    /// <inheritdoc/>
    public BllResult<List<Post>> GetAllPosts(int sectionId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<List<Post>>.Failure("未找到板块");
        }

        var posts = postDalFactory.GetPostDal(tableName).GetAllPosts().Data;
        return BllResult<List<Post>>.Success(posts);
    }

    /// <inheritdoc/>
    public BllResult<List<User>> GetUserInfoByPostId(int sectionId, IEnumerable<int> postIds, string fields)
    {
        // 首先根据postIds获取userIds
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<List<User>>.Failure("未找到板块");
        }

        var userIds = postDalFactory
            .GetPostDal(tableName)
            .GetUserIdByPostId(postIds).Data;
        
        // 然后根据userIds和查询参数获取发帖用户信息
        var users = UserDal
            .GetUserInfoById(userIds!, fields)
            .Data;
        return BllResult<List<User>>.Success(users);

    }

    /// <inheritdoc/>
    public BllResult<List<Post>> GetPagedMainPosts(int sectionId, int pageSize, int pageNumber)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<List<Post>>.Failure("未找到板块");
        }

        var posts = postDalFactory
            .GetPostDal(tableName)
            .GetPagedMainPosts(pageSize, pageNumber).Data;
        return BllResult<List<Post>>.Success(posts);
    }

    /// <inheritdoc/>
    public BllResult<List<Post>> GetPosts(int sectionId, int mainPostId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<List<Post>>.Failure("未找到板块");
        }

        var posts = postDalFactory.GetPostDal(tableName).GetPosts(mainPostId).Data;
        return BllResult<List<Post>>.Success(posts);
    }

    /// <inheritdoc/>
    public BllResult<int> AddPost(int sectionId, Post post)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<int>.Failure("未找到板块");
        }

        var id = postDalFactory.GetPostDal(tableName).AddPost(post).Data;
        var mainPost = postDalFactory.GetPostDal(tableName).GetPostById(post.MainPostId).Data;
        if (mainPost != null)
        {
            // 更新回复数
            mainPost.ReplyNum = GetPosts(sectionId, post.MainPostId).Data?.Count - 1 ?? 0;

            // 更新最后回复时间
            var lastReplyTime = post.PublishTime;
            if (lastReplyTime > mainPost.LastReplyTime)
            {
                mainPost.LastReplyTime = lastReplyTime;
            }

            postDalFactory.GetPostDal(tableName).UpdatePost(mainPost);
        }

        return BllResult<int>.Success(id);
    }

    /// <inheritdoc/>
    public BllResult<Post> GetLastReplyPosts(int sectionId, int mainPostId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<Post>.Failure("未找到板块");
        }

        var post = postDalFactory.GetPostDal(tableName).GetLastReplyPosts(mainPostId).Data;
        return BllResult<Post>.Success(post);
    }

    /// <inheritdoc/>
    public BllResult<object> UpVote(int sectionId, int postId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<object>.Failure("未找到板块");
        }

        var post = postDalFactory.GetPostDal(tableName).GetPostById(postId).Data;
        if (post == null)
        {
            return BllResult<object>.Failure("未找到帖子");
        }
        post.UpVote++;
        postDalFactory.GetPostDal(tableName).UpdatePost(post);
        return BllResult<object>.Success();
    }

    /// <inheritdoc/>
    public BllResult<object> DownVote(int sectionId, int postId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<object>.Failure("未找到板块");
        }

        var post = postDalFactory.GetPostDal(tableName).GetPostById(postId).Data;
        if (post == null)
        {
            return BllResult<object>.Failure("未找到帖子");
        }
        post.DownVote++;
        postDalFactory.GetPostDal(tableName).UpdatePost(post);
        return BllResult<object>.Success();
    }

    /// <inheritdoc/>
    public BllResult<object> UpdateView(int sectionId, int postId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<object>.Failure("未找到板块");
        }

        var post = postDalFactory.GetPostDal(tableName).GetPostById(postId).Data;
        if (post == null)
        {
            return BllResult<object>.Failure("未找到帖子");
        }
        post.ViewNum++;
        postDalFactory.GetPostDal(tableName).UpdatePost(post);
        return BllResult<object>.Success();
    }


}
