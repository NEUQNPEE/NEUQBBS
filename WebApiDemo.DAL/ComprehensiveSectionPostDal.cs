using WebApiDemo.DAL.Interfaces;
using WebApiDemo.DAL.Result;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.EUser;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL;

/// <summary>
/// 帖子数据访问层实现。对应表：ComprehensiveSectionPost
/// </summary>
public class ComprehensiveSectionPostDal : IPostDal
{
    /// <inheritdoc />
    public DalResult<List<Post>> GetAllPosts()
    {
        using var context = DbContextFactory.GetDbContext();
        var posts = context.ComprehensiveSectionPosts.OfType<Post>().ToList();

        return posts.Count != 0
            ? DalResult<List<Post>>.Success(posts)
            : DalResult<List<Post>>.Success([]);
    }

    /// <inheritdoc />
    public DalResult<List<Post>> GetPagedMainPosts(int pageSize, int pageNumber)
    {
        using var context = DbContextFactory.GetDbContext();
        // 查找所有是主贴的帖子，按时间从新到旧排序
        var posts = context.ComprehensiveSectionPosts
            .Where(p => p.IsMainPost)
            .OrderByDescending(p => p.PublishTime)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OfType<Post>()
            .ToList();

        return posts.Count != 0
            ? DalResult<List<Post>>.Success(posts)
            : DalResult<List<Post>>.Success([]);
    }

    /// <inheritdoc />
    public DalResult<List<Post>> GetPosts(int mainPostId)
    {
        using var context = DbContextFactory.GetDbContext();
        var mainPost = context.ComprehensiveSectionPosts.Find(mainPostId);
        if (mainPost == null)
        {
            return DalResult<List<Post>>.Failure("未找到主帖");
        }

        var posts = new List<Post> { mainPost };

        posts.AddRange(
            [.. context.ComprehensiveSectionPosts
                .Where(p => p.MainPostId == mainPostId)
                .OfType<Post>()]
        );

        posts.Sort((p1, p2) => p1?.PublishTime.CompareTo(p2?.PublishTime) ?? 0);
        return DalResult<List<Post>>.Success(posts);
    }

    /// <inheritdoc />
    public DalResult<Post> GetPostById(int postId)
    {
        using var context = DbContextFactory.GetDbContext();
        var post = context.ComprehensiveSectionPosts.Find(postId);
        return post != null ? DalResult<Post>.Success(post) : DalResult<Post>.Failure("未找到该帖子");
    }

    /// <inheritdoc />
    public DalResult<List<Post>> GetPostsByIds(List<int> postIds)
    {
        using var context = DbContextFactory.GetDbContext();
        var posts = context.ComprehensiveSectionPosts
            .Where(p => postIds.Contains(p.Id))
            .OfType<Post>()
            .ToList();
        return posts.Count != 0
            ? DalResult<List<Post>>.Success(posts)
            : DalResult<List<Post>>.Success([]);
    }

    /// <inheritdoc />
    public DalResult<List<int>> GetUserIdByPostId(IEnumerable<int> postIds)
    {
        using var context = DbContextFactory.GetDbContext();
        var userIds = context.ComprehensiveSectionPosts
            .Where(p => postIds.Contains(p.Id))
            .Select(p => p.UserId)
            .ToList();
        return userIds.Count != 0
            ? DalResult<List<int>>.Success(userIds)
            : DalResult<List<int>>.Success([]);
    }

    /// <inheritdoc />
    public DalResult<int> AddPost(Post post)
    {
        using var context = DbContextFactory.GetDbContext();
        context.ComprehensiveSectionPosts.Add(post.ToComprehensiveSectionPost());
        context.SaveChanges();

        var newPostId =
            context.ComprehensiveSectionPosts.OrderByDescending(p => p.Id).FirstOrDefault()?.Id
            ?? -1;

        return newPostId != -1
            ? DalResult<int>.Success(newPostId)
            : DalResult<int>.Failure("添加帖子失败");
    }

    /// <inheritdoc />
    public DalResult<object> SetMainPost(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var post = context.ComprehensiveSectionPosts.Find(id);
        if (post != null)
        {
            post.IsMainPost = true;
            context.SaveChanges();
            return DalResult<object>.Success();
        }
        return DalResult<object>.Failure("未找到帖子");
    }

    /// <inheritdoc />
    public DalResult<List<User>> GetAllUsers()
    {
        using var context = DbContextFactory.GetDbContext();
        var userIds = context.ComprehensiveSectionPosts.Select(p => p.UserId).Distinct().ToList();

        var users = userIds
            .Select(id => context.Users.Find(id))
            .Select(u => u!)
            .ToList();

        return users.Count != 0
            ? DalResult<List<User>>.Success(users)
            : DalResult<List<User>>.Success([]);
    }

    /// <inheritdoc />
    public DalResult<Post> GetLastReplyPosts(int mainPostId)
    {
        using var context = DbContextFactory.GetDbContext();
        var post = context.ComprehensiveSectionPosts
            .Where(p => p.MainPostId == mainPostId)
            .OrderByDescending(p => p.Id)
            .FirstOrDefault();

        return post != null ? DalResult<Post>.Success(post) : DalResult<Post>.Failure("未找到回复");
    }

    /// <inheritdoc />
    public DalResult<List<Post>> GetMainPosts()
    {
        using var context = DbContextFactory.GetDbContext();
        var mainPosts = context.ComprehensiveSectionPosts
            .Where(p => p.IsMainPost)
            .OfType<Post>()
            .ToList();

        return mainPosts.Count != 0
            ? DalResult<List<Post>>.Success(mainPosts)
            : DalResult<List<Post>>.Success([]);
    }

    /// <inheritdoc />
    public DalResult<object> UpdatePost(Post post)
    {
        using var context = DbContextFactory.GetDbContext();
        var postToUpdate = context.ComprehensiveSectionPosts.Find(post.Id);
        if (postToUpdate == null)
        {
            return DalResult<object>.Failure("未找到帖子");
        }

        postToUpdate = post.ToComprehensiveSectionPost();

        context.ComprehensiveSectionPosts.Update(postToUpdate);
        context.SaveChanges();
        return DalResult<object>.Success();
    }

}
