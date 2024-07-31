using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;

namespace WebApiDemo.DAL.Interfaces;

public interface IPostDal
{
    List<Post?>? GetAllPosts();

    List<Post?>? GetPosts(int beginNum, int needNum);

    List<Post?>? GetPosts(int mainPostId);


    Post? GetPostById(int id);

    int AddPost(Post post);
    void SetMainPost(int id);
    List<UserBModel?>? GetAllUsers();
    List<UserBModel?>? GetUsers(int beginNum, int needNum);

    Post? GetLastReplyPosts(int mainPostId);

    List<Post>? GetMainPosts();

    void UpdatePost(Post post);

}