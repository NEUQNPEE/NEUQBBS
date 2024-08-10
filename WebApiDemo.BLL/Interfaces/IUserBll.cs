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
    /// <returns>所有用户的列表，或在无用户时返回空列表。</returns>
    List<UserBModel> DebugGetAllUsers();

    /// <summary>
    /// debug 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户信息，或在用户不存在时返回 null。</returns>
    UserBModel? DebugGetUserById(int id);

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户信息，或在用户不存在时返回 null。</returns>
    UserBModel? GetUserById(int id);

    /// <summary>
    /// 根据用户ID获取用户名
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户名，如果用户不存在则返回 "未找到"。</returns>
    string GetUserNameById(int id);

    /// <summary>
    /// 根据用户ID列表获取用户名列表
    /// </summary>
    /// <param name="ids">用户 ID 列表</param>
    /// <returns>用户名列表，或在无对应用户时返回空列表。</returns>
    List<string> GetUserNamesByIds(List<int> ids);

    /// <summary>
    /// 根据用户ID获取用户用于POST的数据，只保留 userName, RegisterTime 和 Points 字段，注意排除 IsDeleted 字段
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户信息，或在用户不存在时返回 null。</returns>
    UserBModel? GetUserByIdForPost(int id);

    /// <summary>
    /// 根据用户ID获取用户基本信息
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户基本信息，或在用户不存在时返回 null。</returns>
    UserBModel? GetUserBaseInfoById(int id);

    /// <summary>
    /// 根据用户ID获取用户详细信息
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户详细信息，或在用户不存在时返回 null。</returns>
    UserBModel? GetUserDetailInfoById(int id);

    /// <summary>
    /// 检查用户是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>如果用户存在，则返回 true；否则返回 false。</returns>
    bool CheckUserExist(string userName);

    /// <summary>
    /// 检查登录
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>如果登录成功，返回用户 ID；否则返回 -1。</returns>
    int CheckLogin(string userName, string password);

    /// <summary>
    /// 生成自动登录 token
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>生成的自动登录 token。</returns>
    string GenerateAutoLoginToken(int userId);

    /// <summary>
    /// 检查自动登录 token
    /// </summary>
    /// <param name="token">自动登录 token</param>
    /// <returns>有效的用户 ID，如果 token 无效，则返回 0 或负值。</returns>
    int CheckAutoLoginToken(string token);

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>添加的用户 ID，如果添加失败，则返回 -1。</returns>
    int AddUser(UserBModel userBModel);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>更新结果消息，成功时返回 "更新成功"，否则返回 "更新失败"。</returns>
    string UpdateUser(UserBModel userBModel);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>删除结果消息，成功时返回 "删除成功"，否则返回 "删除失败"。</returns>
    string RemoveUser(int id);
}
