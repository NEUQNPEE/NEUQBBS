using System.Data;
using WebApiDemo.DAL.Mapper;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EAuthToken;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL;

/// <summary>
/// 用户数据访问层
/// </summary>
public class UserDal
{
    /// <summary>
    /// 获取所有用户（调试用）
    /// </summary>
    /// <returns>用户列表</returns>
    public static List<UserBModel> DebugGetAllUsers()
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users.ToList();
        return users
            .Select(user => user?.ToUserBModel())
            .Where(UserBModel => UserBModel != null)
            .ToList()!;
    }

    /// <summary>
    /// 根据用户 ID 获取用户（调试用）
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户信息，或在无匹配用户时返回 null</returns>
    public static UserBModel? DebugGetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Users.Find(id)?.ToUserBModel();
    }

    /// <summary>
    /// 根据用户 ID 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>用户信息，或在无匹配用户时返回 null</returns>
    public static UserBModel? GetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Users.Find(id)?.ToPublicUserBModel();
    }

    /// <summary>
    /// 根据用户名和密码获取用户
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>用户列表，或在无匹配用户时返回空列表</returns>
    public static List<UserBModel>? GetUserByUserNameAndPassword(string userName, string password)
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users
            .Where(user => user.UserName == userName && user.Password == password)
            .ToList();
        return users.Select(user => user.ToUserBModel()).ToList();
    }

    /// <summary>
    /// 生成一个自动登录 Token
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>生成的 Token</returns>
    public static string GenerateAutoLoginToken(int userId)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(userId);
        if (user == null)
        {
            return string.Empty;
        }
        string token = Guid.NewGuid().ToString();
        context.AuthTokens.Add(
            new AuthToken
            {
                UserId = userId,
                Token = token,
                ExpireTime = DateTime.Now.AddDays(7)
            }
        );
        context.SaveChanges();
        return token;
    }

    /// <summary>
    /// 检查 Token 登录
    /// </summary>
    /// <param name="token">Token</param>
    /// <returns>用户 ID，或在无效 Token 时返回 -1</returns>
    public static int CheckAutoLoginToken(string token)
    {
        using var context = DbContextFactory.GetDbContext();
        var authToken = context.AuthTokens.FirstOrDefault(authToken => authToken.Token == token);
        if (authToken == null)
        {
            return -1;
        }
        if (authToken.ExpireTime < DateTime.Now)
        {
            context.AuthTokens.Remove(authToken);
            context.SaveChanges();
            return -1;
        }
        return authToken.UserId;
    }

    /// <summary>
    /// 检查用户是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>如果用户存在则返回 true，否则返回 false</returns>
    public static bool CheckUserExist(string userName)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Users.Any(user => user.UserName == userName);
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>新增用户的 ID</returns>
    public static int AddUser(UserBModel userBModel)
    {
        using var context = DbContextFactory.GetDbContext();
        context.Users.Add(userBModel.ToUser());
        context.SaveChanges();
        return context.Users.Max(user => user.Id);
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>影响的行数</returns>
    public static int UpdateUser(UserBModel userBModel)
    {
        using var context = DbContextFactory.GetDbContext();
        var userToUpdate = context.Users.Find(userBModel.Id);
        if (userToUpdate == null)
        {
            return 0;
        }
        context.Users.Update(userToUpdate.UpdateFromBModel(userBModel));
        return context.SaveChanges();
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>影响的行数</returns>
    public static int RemoveUser(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var userToRemove = context.Users.Find(id);
        if (userToRemove == null)
        {
            return 0;
        }
        context.Users.Remove(userToRemove);
        return context.SaveChanges();
    }

    /// <summary>
    /// 按 ID 设置最后登录时间
    /// </summary>
    /// <param name="id">用户 ID</param>
    public static void UpdateLastLoginTime(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(id);
        if (user == null)
        {
            return;
        }
        user.LastLoginTime = DateTime.Now;
        context.Users.Update(user);
        context.SaveChanges();
    }
}
