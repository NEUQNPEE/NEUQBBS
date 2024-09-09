using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;
using WebApiDemo.DAL.Result;

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
    /// 获取指定范围内的帖子
    /// </summary>
    /// <param name="beginNum">起始数量</param>
    /// <param name="needNum">需要数量</param>
    /// <returns>操作结果，包含帖子列表，或在无帖子时返回空列表</returns>
    DalResult<List<Post>> GetPosts(int beginNum, int needNum);

    /// <summary>
    /// 根据主帖 ID 获取帖子
    /// </summary>
    /// <param name="mainPostId">主帖 ID</param>
    /// <returns>操作结果，包含帖子列表，或在无帖子时返回空列表</returns>
    DalResult<List<Post>> GetPosts(int mainPostId);

    /// <summary>
    /// 根据 ID 获取帖子
    /// </summary>
    /// <param name="id">帖子 ID</param>
    /// <returns>操作结果，包含帖子信息，或在无匹配帖子时返回失败消息</returns>
    DalResult<Post> GetPostById(int id);

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
    DalResult<List<UserBModel>> GetAllUsers();

    /// <summary>
    /// 获取指定范围内的用户
    /// </summary>
    /// <param name="beginNum">起始帖子数量</param>
    /// <param name="needNum">需要帖子数量</param>
    /// <returns>操作结果，包含用户列表，或在无用户时返回空列表</returns>
    DalResult<List<UserBModel>> GetUsers(int beginNum, int needNum);

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
