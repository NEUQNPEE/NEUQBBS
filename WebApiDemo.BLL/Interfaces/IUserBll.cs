using WebApiDemo.Entities;
using WebApiDemo.Entities.BModels;

namespace WebApiDemo.BLL.Interfaces
{
    /// <summary>
    /// 用户相关业务逻辑的接口
    /// </summary>
    public interface IUserBll
    {
        /// <summary>
        /// debug 获取所有用户
        /// </summary>
        /// <returns></returns>
        List<UserBModel> DebugGetAllUsers();

        /// <summary>
        /// debug 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserBModel? DebugGetUserById(int id);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserBModel? GetUserById(int id);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        string GetUserNameById(int id);

        /// <summary>
        /// 根据用户ID列表获取用户名列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<string> GetUserNamesByIds(List<int> ids);

        /// <summary>
        /// 根据用户ID获取用户用于POST的数据，只保留userName,RigisterTime和Points字段，注意排除IsDeleted字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserBModel? GetUserByIdForPost(int id);

        /// <summary>
        /// 根据用户ID获取用户基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserBModel? GetUserBaseInfoById(int id);

        /// <summary>
        /// 根据用户ID获取用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserBModel? GetUserDetailInfoById(int id);

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool CheckUserExist(string userName);

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int CheckLogin(string userName, string password);

        /// <summary>
        /// 生成自动登录token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GenerateAutoLoginToken(int userId);

        /// <summary>
        /// 检查自动登录token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int CheckAutoLoginToken(string token);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userBModel"></param>
        /// <returns></returns>
        int AddUser(UserBModel userBModel);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userBModel"></param>
        /// <returns></returns>
        string UpdateUser(UserBModel userBModel);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string RemoveUser(int id);
    }
}
