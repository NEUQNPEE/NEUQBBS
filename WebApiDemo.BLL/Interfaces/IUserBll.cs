using WebApiDemo.BLL.Result;
using WebApiDemo.Entities.BModels;

namespace WebApiDemo.BLL.Interfaces;

/// <summary>
/// 用户业务逻辑接口
/// </summary>
public interface IUserBll
{
    /// <summary>
    /// debug 获取所有用户
    /// </summary>
    /// <returns>包含所有用户列表的操作结果，或在无用户时返回空列表。</returns>
    BllResult<List<UserBModel>> DebugGetAllUsers();

    /// <summary>
    /// debug 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>包含用户信息的操作结果，或在用户不存在时返回 null。</returns>
    BllResult<UserBModel> DebugGetUserById(int id);

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>包含用户信息的操作结果，或在用户不存在时返回 null。</returns>
    BllResult<UserBModel> GetUserById(int id);

    /// <summary>
    /// 根据用户ID获取用户名
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>包含用户名的操作结果，如果用户不存在则返回 "未找到"。</returns>
    BllResult<string> GetUserNameById(int id);

    /// <summary>
    /// 根据用户ID列表获取用户名列表
    /// </summary>
    /// <param name="ids">用户 ID 列表</param>
    /// <returns>包含用户名列表的操作结果，或在无对应用户时返回空列表。</returns>
    BllResult<List<string>> GetUserNamesByIds(List<int> ids);

    /// <summary>
    /// 根据用户ID获取用户用于POST的数据
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>包含用户信息的操作结果，或在用户不存在时返回 null。</returns>
    BllResult<UserBModel> GetUserByIdForPost(int id);

    /// <summary>
    /// 根据用户ID获取用户基本信息
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>包含用户基本信息的操作结果，或在用户不存在时返回 null。</returns>
    BllResult<UserBModel> GetUserBaseInfoById(int id);

    /// <summary>
    /// 根据用户ID获取用户详细信息
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>包含用户详细信息的操作结果，或在用户不存在时返回 null。</returns>
    BllResult<UserBModel> GetUserDetailInfoById(int id);

    /// <summary>
    /// 检查用户是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>包含用户存在状态的操作结果。</returns>
    BllResult<bool> CheckUserExist(string userName);

    /// <summary>
    /// 检查登录
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>登录结果，成功返回用户 ID，失败返回 -1。</returns>
    BllResult<int> CheckLogin(string userName, string password);

    /// <summary>
    /// 生成自动登录 token
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>包含自动登录 token 的操作结果。</returns>
    BllResult<string> GenerateAutoLoginToken(int userId);

    /// <summary>
    /// 检查自动登录 token
    /// </summary>
    /// <param name="token">自动登录 token</param>
    /// <returns>有效用户 ID 的操作结果，如果 token 无效则返回 0 或负值。</returns>
    BllResult<int> CheckAutoLoginToken(string token);

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>包含添加用户 ID 的操作结果，如果添加失败则返回 -1。</returns>
    BllResult<int> AddUser(UserBModel userBModel);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>更新结果的操作结果，成功返回 "更新成功"，否则返回 "更新失败"。</returns>
    BllResult<string> UpdateUser(UserBModel userBModel);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>删除结果的操作结果，成功返回 "删除成功"，否则返回 "删除失败"。</returns>
    BllResult<string> RemoveUser(int id);
}
