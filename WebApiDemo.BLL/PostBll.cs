using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.DAL.Interfaces;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;

namespace WebApiDemo.BLL
{
    public class PostBll : IPostBll
    {
        private readonly IPostDalFactory _postDalFactory;

        public PostBll(IPostDalFactory postDalFactory)
        {
            _postDalFactory = postDalFactory;
        }

        public List<Post?>? GetAllPosts(int sectionId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }
            return _postDalFactory.GetPostDal(tableName).GetAllPosts();
        }

        public List<UserBModel?>? GetAllUsers(int sectionId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }
            List<UserBModel?>? users = _postDalFactory.GetPostDal(tableName).GetAllUsers();
            // 保留Id,userName,RigisterTime和Points字段,注意排除IsDeleted字段
            if (users == null)
            {
                return null;
            }

            List<UserBModel?>? result = new();
            foreach (UserBModel? user in users)
            {
                if (user == null)
                {
                    continue;
                }
                result.Add(
                    new UserBModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        RegisterTime = user.RegisterTime,
                        Points = user.Points
                    }
                );
            }

            return result;
        }

        public List<Post?>? GetPosts(PostListBModel postListBModel)
        {
            string? tableName = SectionDal.GetSectionById(postListBModel.SectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }
            return _postDalFactory
                .GetPostDal(tableName)
                .GetPosts(postListBModel.BeginNum, postListBModel.NeedNum);
        }

        public List<Post?>? GetPosts(int sectionId, int mainPostId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }
            return _postDalFactory.GetPostDal(tableName).GetPosts(mainPostId);
        }

        public int AddPost(int sectionId, Post post)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return -1;
            }
            int id =_postDalFactory.GetPostDal(tableName).AddPost(post);
            Post? mainPost = _postDalFactory
                .GetPostDal(tableName)
                .GetPostById(post.MainPostId);
            if (mainPost != null)
            {
                mainPost.ReplyNum = GetPosts(sectionId, post.MainPostId)?.Count-1 ?? 0;
                _postDalFactory.GetPostDal(tableName).UpdatePost(mainPost);
            }
            
            return id;
        }

        public List<UserBModel?>? GetUsers(PostListBModel postListBModel)
        {
            string? tableName = SectionDal.GetSectionById(postListBModel.SectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }
            List<UserBModel?>? users = _postDalFactory
                .GetPostDal(tableName)
                .GetUsers(postListBModel.BeginNum, postListBModel.NeedNum);
            // 保留Id,userName,RigisterTime和Points字段,注意排除IsDeleted字段
            if (users == null)
            {
                return null;
            }

            List<UserBModel?>? result = new();
            foreach (UserBModel? user in users)
            {
                if (user == null)
                {
                    continue;
                }
                result.Add(
                    new UserBModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        RegisterTime = user.RegisterTime,
                        Points = user.Points
                    }
                );
            }

            return result;
        }

        public Post? GetLastReplyPosts(int sectionId, int mainPostId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }

            return _postDalFactory.GetPostDal(tableName).GetLastReplyPosts(mainPostId);
        }

        public void UpVote(int sectionId, int postId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return;
            }

            Post? post = _postDalFactory.GetPostDal(tableName).GetPostById(postId);
            if (post == null)
            {
                return;
            }
            post.UpVote++;
            _postDalFactory.GetPostDal(tableName).UpdatePost(post);
        }

        public void DownVote(int sectionId, int postId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return;
            }

            Post? post = _postDalFactory.GetPostDal(tableName).GetPostById(postId);
            if (post == null)
            {
                return;
            }
            post.DownVote++;
            _postDalFactory.GetPostDal(tableName).UpdatePost(post);
        }

        public void UpdateView(int sectionId, int postId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return;
            }

            Post? post = _postDalFactory.GetPostDal(tableName).GetPostById(postId);
            if (post == null)
            {
                return;
            }
            post.ViewNum++;
            _postDalFactory.GetPostDal(tableName).UpdatePost(post);
        }
    }
}
