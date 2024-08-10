using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;

namespace WebApiDemo.BLL.Interfaces;

/// <summary>
/// 帖子业务逻辑接口
/// </summary>
public interface IPostBll
{
    /// <summary>
    /// 获取指定版块的所有帖子。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <returns>帖子列表，或在无帖子时返回空列表。</returns>
    List<Post>? GetAllPosts(int sectionId);

    /// <summary>
    /// 获取在指定版块发过帖的所有用户的部分信息，包括：Id, UserName, RegisterTime和Points
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <returns>用户业务模型列表，或在无用户时返回空列表。</returns>
    List<UserBModel>? GetAllUsers(int sectionId);

    /// <summary>
    /// 根据帖子列表业务模型获取帖子。用于分页功能。
    /// </summary>
    /// <param name="postListBModel">帖子列表业务模型</param>
    /// <returns>符合条件的帖子列表，或在无匹配帖子时返回空列表。</returns>
    List<Post>? GetPosts(PostListBModel postListBModel);

    /// <summary>
    /// 获取指定版块和主帖 ID 的帖子。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <param name="mainPostId">主帖 ID</param>
    /// <returns>帖子列表，或在无匹配帖子时返回 null。</returns>
    List<Post>? GetPosts(int sectionId, int mainPostId);

    /// <summary>
    /// 在指定版块中添加新帖。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <param name="post">要添加的帖子</param>
    /// <returns>添加的帖子的 ID。</returns>
    int AddPost(int sectionId, Post post);

    /// <summary>
    /// 根据帖子列表业务模型获取用户。用于获取一个分页上的帖子的所属用户。
    /// </summary>
    /// <param name="postListBModel">帖子列表业务模型</param>
    /// <returns>用户业务模型列表，或在无匹配用户时返回空列表。</returns>
    List<UserBModel>? GetUsers(PostListBModel postListBModel);

    /// <summary>
    /// 获取指定版块和主帖 ID 的最后回复帖。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <param name="mainPostId">主帖 ID</param>
    /// <returns>最后回复的帖子，或在无回复时返回 null。</returns>
    Post? GetLastReplyPosts(int sectionId, int mainPostId);

    /// <summary>
    /// 为指定版块和帖子 ID 点赞。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <param name="postId">帖子 ID</param>
    void UpVote(int sectionId, int postId);

    /// <summary>
    /// 为指定版块和帖子 ID 点踩。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <param name="postId">帖子 ID</param>
    void DownVote(int sectionId, int postId);

    /// <summary>
    /// 更新指定版块和帖子 ID 的浏览量。
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <param name="postId">帖子 ID</param>
    void UpdateView(int sectionId, int postId);
}
