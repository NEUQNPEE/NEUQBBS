using WebApiDemo.DAL.Interfaces;
using WebApiDemo.DAL.Mapper;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL;
/// <summary>
/// 帖子数据访问层实现。对应表：ComprehensiveSectionPost
/// </summary>
public class ComprehensiveSectionPostDal : IPostDal
{
    /// <inheritdoc />
    public List<Post>? GetAllPosts()
    {
        using var context = DbContextFactory.GetDbContext();
        List<Post> posts = context.ComprehensiveSectionPosts.ToList().Select(p => p as Post).Where(p => p != null).Select(p => p!).ToList();
        return posts;
    }

    /// <inheritdoc />
    public List<Post>? GetPosts(int beginIndex, int needNum)
    {
        using var context = DbContextFactory.GetDbContext();
        List<Post> posts = context.ComprehensiveSectionPosts
            .Skip(beginIndex)
            .Take(needNum)
            .ToList()
            .Select(p => p as Post)
            .Where(p => p != null)
            .Select(p => p!)
            .ToList();
        return posts;
    }

    /// <inheritdoc />
    public List<Post>? GetPosts(int mainPostId)
    {
        using var context = DbContextFactory.GetDbContext();
        Post? mainPost = context.ComprehensiveSectionPosts.Find(mainPostId);
        if (mainPost == null)
        {
            return null;
        }

        List<Post> posts = new() { mainPost };

        posts.AddRange(context.ComprehensiveSectionPosts
            .Where(p => p.MainPostId == mainPostId)
            .ToList()
            .Select(p => p as Post)
            .Where(p => p != null)
            .Select(p => p!)
            .ToList());

        // 按发布时间排序
        posts.Sort((p1, p2) => p1?.PublishTime.CompareTo(p2?.PublishTime) ?? 0);
        return posts;
    }

    /// <inheritdoc />
    public Post? GetPostById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.ComprehensiveSectionPosts.Find(id);
    }

    /// <inheritdoc />
    public int AddPost(Post post)
    {
        using var context = DbContextFactory.GetDbContext();
        context.ComprehensiveSectionPosts.Add(post.ToComprehensiveSectionPost());
        context.SaveChanges();

        // 立即查询刚刚插入的数据的Id
        return context.ComprehensiveSectionPosts
                .OrderByDescending(p => p.Id)
                .FirstOrDefault()
                ?.Id ?? -1;
    }

    /// <inheritdoc />
    public void SetMainPost(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var post = context.ComprehensiveSectionPosts.Find(id);
        if (post != null)
        {
            post.IsMainPost = true;
            context.SaveChanges();
        }
    }

    /// <inheritdoc />
    public List<UserBModel>? GetAllUsers()
    {
        using var context = DbContextFactory.GetDbContext();
        var userIds = context.ComprehensiveSectionPosts
            .Select(p => p.UserId)
            .Distinct()
            .ToList();
        var users = userIds.Select(id => context.Users.Find(id)?.ToUserBModel()).Where(u => u != null).Select(u => u!).ToList();
        return users;
    }

    /// <inheritdoc />
    public List<UserBModel>? GetUsers(int beginNum, int needNum)
    {
        using var context = DbContextFactory.GetDbContext();
        var userIds = context.ComprehensiveSectionPosts
            .Select(p => p.UserId)
            .Distinct()
            .Skip(beginNum)
            .Take(needNum)
            .ToList();
        var users = userIds.Select(id => context.Users.Find(id)?.ToUserBModel()).Where(u => u != null).Select(u => u!).ToList();
        return users;
    }

    /// <inheritdoc />
    public Post? GetLastReplyPosts(int mainPostId)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.ComprehensiveSectionPosts
            .Where(p => p.MainPostId == mainPostId)
            .OrderByDescending(p => p.Id)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public List<Post>? GetMainPosts()
    {
        using var context = DbContextFactory.GetDbContext();
        return context.ComprehensiveSectionPosts
            .Where(p => p.IsMainPost)
            .ToList()
            .OfType<Post>()
            .ToList();
    }

    /// <inheritdoc />
    public void UpdatePost(Post post)
    {
        using var context = DbContextFactory.GetDbContext();
        var postToUpdate = context.ComprehensiveSectionPosts.Find(post.Id);
        if (postToUpdate != null)
        {
            postToUpdate.UpVote = post.UpVote;
            postToUpdate.DownVote = post.DownVote;
            postToUpdate.ViewNum = post.ViewNum;
            postToUpdate.ReplyNum = post.ReplyNum;
            context.ComprehensiveSectionPosts.Update(postToUpdate);
            context.SaveChanges();
        }
    }
}
