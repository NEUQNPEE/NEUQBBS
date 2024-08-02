using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;

namespace WebApiDemo.BLL.Interfaces
{
    public interface IPostBll
    {
        List<Post>? GetAllPosts(int sectionId);
        List<UserBModel>? GetAllUsers(int sectionId);

        List<Post>? GetPosts(PostListBModel postListBModel);
        List<Post>? GetPosts(int sectionId, int mainPostId);

        int AddPost(int sectionId,Post post);
        
        List<UserBModel>? GetUsers(PostListBModel postListBModel);

        // 获取最后回复帖
        Post? GetLastReplyPosts(int sectionId, int mainPostId);

        void UpVote(int sectionId,int postId);

        void DownVote(int sectionId,int postId);
        void UpdateView(int sectionId, int postId);
    }
}