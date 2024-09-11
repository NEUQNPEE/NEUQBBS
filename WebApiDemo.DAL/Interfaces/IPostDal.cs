using WebApiDemo.Entities.EPost;
using WebApiDemo.DAL.Result;
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.DAL.Interfaces;

/// <summary>
/// 帖子数据访问层接口
/// </summary>
public interface IPostDal
{
    /// <summary>
    /// 获取所有帖子
    /// </summary>
    /// <returns>操作结果，包含帖子列表，或在无帖子时返回空列表</returns>
    DalResult<List<Post>> GetAllPosts();

    /// <summary>
    /// 获取指定版块的主贴，分页
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    DalResult<List<Post>> GetPagedMainPosts(int pageSize, int pageNumber);

    /// <summary>
    /// 根据主帖 ID 获取帖子
    /// </summary>
    /// <param name="mainPostId">主帖 ID</param>
    /// <returns>操作结果，包含帖子列表，或在无帖子时返回空列表</returns>
    DalResult<List<Post>> GetPosts(int mainPostId);

    /// <summary>
    /// 根据 postId 获取帖子
    /// </summary>
    /// <param name="postId">帖子 ID</param>
    /// <returns>操作结果，包含帖子信息，或在无匹配帖子时返回失败消息</returns>
    DalResult<Post> GetPostById(int postId);

    /// <summary>
    /// 根据 postIds 获取帖子
    /// </summary>
    /// <param name="postIds">帖子 ID 列表</param>
    /// <returns>操作结果，包含帖子列表，或在无帖子时返回空列表</returns>
    DalResult<List<Post>> GetPostsByIds(List<int> postIds);

    /// <summary>
    /// 根据 postIds 获取 userIds
    /// </summary>
    /// <param name="postIds">帖子 ID 列表</param>
    /// <returns>操作结果，包含用户 ID 列表，或在无用户时返回空列表</returns>
    DalResult<List<int>> GetUserIdByPostId(IEnumerable<int> postIds);

    /// <summary>
    /// 添加帖子
    /// </summary>
    /// <param name="post">帖子信息</param>
    /// <returns>操作结果，包含新添加帖子的 ID</returns>
    DalResult<int> AddPost(Post post);

    /// <summary>
    /// 设置主帖
    /// </summary>
    /// <param name="id">帖子 ID</param>
    /// <returns>操作结果，成功或失败</returns>
    DalResult<object> SetMainPost(int id);

    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <returns>操作结果，包含用户列表，或在无用户时返回空列表</returns>
    DalResult<List<User>> GetAllUsers();

    /// <summary>
    /// 获取最后的回复帖子
    /// </summary>
    /// <param name="mainPostId">主帖 ID</param>
    /// <returns>操作结果，包含最后回复的帖子，或在无回复时返回失败消息</returns>
    DalResult<Post> GetLastReplyPosts(int mainPostId);

    /// <summary>
    /// 获取所有主帖
    /// </summary>
    /// <returns>操作结果，包含主帖列表，或在无主帖时返回空列表</returns>
    DalResult<List<Post>> GetMainPosts();

    /// <summary>
    /// 更新帖子信息
    /// </summary>
    /// <param name="post">更新后的帖子信息</param>
    /// <returns>操作结果，成功或失败</returns>
    DalResult<object> UpdatePost(Post post);
}
