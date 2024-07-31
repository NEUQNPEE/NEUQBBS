using WebApiDemo.DAL.Interfaces;
using WebApiDemo.DAL.Mapper;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL
{
    public class ComprehensiveSectionPostDal : IPostDal
    {
        public List<Post?>? GetAllPosts()
        {
            using var context = DbContextFactory.GetDbContext();
            List<Post?> posts = context.ComprehensiveSectionPosts.ToList().OfType<Post?>().ToList();
            if (posts == null)
            {
                return null;
            }
            return posts;
        }

        public List<Post?>? GetPosts(int beginIndex, int needNum)
        {
            using var context = DbContextFactory.GetDbContext();
            List<Post?> posts = context.ComprehensiveSectionPosts
                .Skip(beginIndex)
                .Take(needNum)
                .ToList()
                .OfType<Post?>()
                .ToList();
            if (posts == null)
            {
                return null;
            }
            return posts;
        }

        public List<Post?>? GetPosts(int mainPostId)
        {
            using var context = DbContextFactory.GetDbContext();
            Post? mainPost = context.ComprehensiveSectionPosts.Find(mainPostId);
            if (mainPost == null)
            {
                return null;
            }

            List<Post?> posts = new List<Post?> { mainPost };

            posts.AddRange(context.ComprehensiveSectionPosts
                .Where(p => p.MainPostId == mainPostId)
                .ToList()
                .OfType<Post?>()
                .ToList());

            // 按发布时间排序
            posts.Sort((p1, p2) => p1?.PublishTime.CompareTo(p2?.PublishTime) ?? 0);
            return posts;
        }

        public Post? GetPostById(int id)
        {
            using var context = DbContextFactory.GetDbContext();
            return context.ComprehensiveSectionPosts.Find(id);
        }

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

        public List<UserBModel?>? GetAllUsers()
        {
            using var context = DbContextFactory.GetDbContext();
            var userIds = context.ComprehensiveSectionPosts
                .Select(p => p.UserId)
                .Distinct()
                .ToList();
            var users = userIds.Select(id => context.Users.Find(id)?.ToUserBModel()).ToList();
            if (users == null)
            {
                return null;
            }
            return users;
        }

        public List<UserBModel?>? GetUsers(int beginNum, int needNum)
        {
            using var context = DbContextFactory.GetDbContext();
            var userIds = context.ComprehensiveSectionPosts
                .Select(p => p.UserId)
                .Distinct()
                .Skip(beginNum)
                .Take(needNum)
                .ToList();
            var users = userIds.Select(id => context.Users.Find(id)?.ToUserBModel()).ToList();
            if (users == null)
            {
                return null;
            }
            return users;
        }

        public Post? GetLastReplyPosts(int mainPostId)
        {
            using var context = DbContextFactory.GetDbContext();
            return context.ComprehensiveSectionPosts
                .Where(p => p.MainPostId == mainPostId)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();
        }

        public List<Post>? GetMainPosts()
        {
            using var context = DbContextFactory.GetDbContext();
            return context.ComprehensiveSectionPosts
                .Where(p => p.IsMainPost)
                .ToList()
                .OfType<Post>()
                .ToList();
        }

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
}
