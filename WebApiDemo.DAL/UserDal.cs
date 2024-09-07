using System.Data;
using WebApiDemo.DAL.Mapper;
using WebApiDemo.DAL.Result;
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
    /// <returns>操作结果，包含用户列表</returns>
    public static DalResult<List<UserBModel>> DebugGetAllUsers()
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users.ToList();
        var userBModels = users
            .Select(user => user.ToUserBModel())
            .Where(userBModel => userBModel != null)
            .ToList();
        return DalResult<List<UserBModel>>.Success(userBModels);
    }

    /// <summary>
    /// 根据用户 ID 获取用户（调试用）
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>操作结果，包含用户信息或错误消息</returns>
    public static DalResult<UserBModel> DebugGetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(id);
        if (user == null)
        {
            return DalResult<UserBModel>.Failure("未找到该用户");
        }
        return DalResult<UserBModel>.Success(user.ToUserBModel());
    }

    /// <summary>
    /// 根据用户 ID 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>操作结果，包含用户公开信息或错误消息</returns>
    public static DalResult<UserBModel> GetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(id);
        if (user == null)
        {
            return DalResult<UserBModel>.Failure("未找到该用户");
        }
        return DalResult<UserBModel>.Success(user.ToPublicUserBModel());
    }

    /// <summary>
    /// 根据用户名和密码获取用户
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>操作结果，包含用户列表或错误消息</returns>
    public static DalResult<List<UserBModel>> GetUserByUserNameAndPassword(
        string userName,
        string password
    )
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users
            .Where(user => user.UserName == userName && user.Password == password)
            .ToList();
        if (users.Count == 0)
        {
            return DalResult<List<UserBModel>>.Failure("用户名或密码错误");
        }

        var userBModels = users.Select(user => user.ToUserBModel()).ToList();
        return DalResult<List<UserBModel>>.Success(userBModels);
    }

    /// <summary>
    /// 生成一个自动登录 Token
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>操作结果，包含生成的 Token 或错误消息</returns>
    public static DalResult<string> GenerateAutoLoginToken(int userId)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(userId);
        if (user == null)
        {
            return DalResult<string>.Failure("用户不存在");
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
        return DalResult<string>.Success(token);
    }

    /// <summary>
    /// 检查 Token 登录
    /// </summary>
    /// <param name="token">Token</param>
    /// <returns>操作结果，包含用户 ID 或错误消息</returns>
    public static DalResult<int> CheckAutoLoginToken(string token)
    {
        using var context = DbContextFactory.GetDbContext();
        var authToken = context.AuthTokens.FirstOrDefault(authToken => authToken.Token == token);
        if (authToken == null)
        {
            return DalResult<int>.Failure("无效的 Token");
        }
        if (authToken.ExpireTime < DateTime.Now)
        {
            context.AuthTokens.Remove(authToken);
            context.SaveChanges();
            return DalResult<int>.Failure("无效的 Token");
        }
        return DalResult<int>.Success(authToken.UserId);
    }

    /// <summary>
    /// 检查用户是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>操作结果，成功或失败</returns>
    public static DalResult<object> CheckUserExist(string userName)
    {
        using var context = DbContextFactory.GetDbContext();
        if (context.Users.Any(user => user.UserName == userName))
        {
            return DalResult<object>.Success();
        }
        return DalResult<object>.Failure("用户不存在");
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>操作结果，包含用户 ID 或错误消息</returns>
    public static DalResult<int> AddUser(UserBModel userBModel)
    {
        using var context = DbContextFactory.GetDbContext();
        context.Users.Add(userBModel.ToUser());
        context.SaveChanges();
        return DalResult<int>.Success(context.Users.Max(user => user.Id));
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="userBModel">用户模型</param>
    /// <returns>操作结果，包含影响的行数或错误消息</returns>
    public static DalResult<int> UpdateUser(UserBModel userBModel)
    {
        using var context = DbContextFactory.GetDbContext();
        var userToUpdate = context.Users.Find(userBModel.Id);
        if (userToUpdate == null)
        {
            return DalResult<int>.Failure("用户不存在");
        }
        context.Users.Update(userToUpdate.UpdateFromBModel(userBModel));
        return DalResult<int>.Success(context.SaveChanges());
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>操作结果，包含影响的行数或错误消息</returns>
    public static DalResult<int> RemoveUser(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var userToRemove = context.Users.Find(id);
        if (userToRemove == null)
        {
            return DalResult<int>.Failure("用户不存在");
        }
        context.Users.Remove(userToRemove);
        return DalResult<int>.Success(context.SaveChanges());
    }

    /// <summary>
    /// 按 ID 设置最后登录时间
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>操作结果，包含影响的行数或错误消息</returns>
    public static DalResult<int> UpdateLastLoginTime(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(id);
        if (user == null)
        {
            return DalResult<int>.Failure("用户不存在");
        }
        user.LastLoginTime = DateTime.Now;
        context.Users.Update(user);
        return DalResult<int>.Success(context.SaveChanges());
    }
}
