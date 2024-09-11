using System.Data;
using WebApiDemo.DAL.Result;
using WebApiDemo.Entities.EAuthToken;
using WebApiDemo.Entities.EUser;
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
    public static DalResult<List<User>> DebugGetAllUsers()
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users.ToList();
        var Users = users
            .Where(User => User != null)
            .ToList();
        return DalResult<List<User>>.Success(Users);
    }

    /// <summary>
    /// 根据用户 ID 获取用户（调试用）
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <returns>操作结果，包含用户信息或错误消息</returns>
    public static DalResult<User> DebugGetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(id);
        if (user == null)
        {
            return DalResult<User>.Failure("未找到该用户");
        }
        return DalResult<User>.Success(user);
    }

    /// <summary>
    /// 根据 userId 获取用户
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>操作结果，包含用户信息或错误消息</returns>
    public static DalResult<User> GetUserById(int userId)
    {
        using var context = DbContextFactory.GetDbContext();
        var user = context.Users.Find(userId);
        if (user == null)
        {
            return DalResult<User>.Failure("未找到该用户");
        }
        return DalResult<User>.Success(user);
    }

    /// <summary>
    /// 根据 userIds 获取用户
    /// </summary>
    /// <param name="userIds">用户 ID 列表</param>
    /// <returns>用户列表，在没有对应用户时返回空列表</returns>
    public static DalResult<List<User>> GetUsersByIds(List<int> userIds)
    {
        using var context = DbContextFactory.GetDbContext();
        return DalResult<List<User>>.Success([.. context.Users.Where(user => userIds.Contains(user.Id))]);
    }

    /// <summary>
    /// 根据用户 ID 和查询参数获取用户
    /// </summary>
    /// <param name="userId">用户 ID 列表</param>
    /// <param name="fields">查询字段</param>
    /// <returns>根据查询参数选择性填充信息的用户模型；没有对应用户时返回失败信息</returns>
    public static DalResult<User> GetUserInfoById(int userId, string fields)
    {
        using var context = DbContextFactory.GetDbContext();
        var fieldsList = fields.ToLower().Split(',');

        var user = context.Users
            .Where(user => user.Id == userId)
            .Select(u => new User
            {
                Id = u.Id,
                UserName = fieldsList.Contains("username") ? u.UserName : null,
                NickName = fieldsList.Contains("nickname") ? u.NickName : null,
                Gender = fieldsList.Contains("gender") ? u.Gender : null,
                Signature = fieldsList.Contains("signature") ? u.Signature : null,
                Avatar = fieldsList.Contains("avatar") ? u.Avatar : null,
                RegisterTime = fieldsList.Contains("registertime") ? u.RegisterTime : DateTime.MinValue,
                LastLoginTime = fieldsList.Contains("lastlogintime") ? u.LastLoginTime : DateTime.MinValue,
                UserLevel = fieldsList.Contains("userlevel") ? u.UserLevel : -1,
                Points = fieldsList.Contains("points") ? u.Points : -1
            })
            .FirstOrDefault();
            
        if (user == null)
        {
            return DalResult<User>.Failure("未找到该用户");
        }
        return DalResult<User>.Success(user);
    }

    /// <summary>
    /// 根据用户 ID 列表和查询参数获取用户
    /// </summary>
    /// <param name="userIds">用户 ID 列表</param>
    /// <param name="fields">查询字段</param>
    /// <returns>根据查询参数选择性填充信息的用户列表；没有对应用户时返回空列表</returns>
    public static DalResult<List<User>> GetUserInfoById(IEnumerable<int> userIds, string fields)
    {
        using var context = DbContextFactory.GetDbContext();
        // 字符串转小写，按','拆分查询字段
        // var fieldsList = fields.Split(',');
        var fieldsList = fields.ToLower().Split(',');

        // 获取User,只填充查询字段
var result = context.Users
    .Where(user => userIds.Contains(user.Id))
    .Select(u => new 
    {
        u.Id,
        u.UserName,
        u.NickName,
        u.Gender,
        u.Signature,
        u.Avatar,
        u.RegisterTime,
        u.LastLoginTime,
        u.UserLevel,
        u.Points
    })
    .ToList()
    .Select(u => new User
    {
        Id = u.Id,
        UserName = fieldsList.Contains("username") ? u.UserName : null,
        NickName = fieldsList.Contains("nickname") ? u.NickName : null,
        Gender = fieldsList.Contains("gender") ? u.Gender : null,
        Signature = fieldsList.Contains("signature") ? u.Signature : null,
        Avatar = fieldsList.Contains("avatar") ? u.Avatar : null,
        RegisterTime = fieldsList.Contains("registertime") ? u.RegisterTime  : DateTime.MinValue,
        LastLoginTime = fieldsList.Contains("lastlogintime") ? u.LastLoginTime : DateTime.MinValue,
        UserLevel = fieldsList.Contains("userlevel") ? u.UserLevel : -1,
        Points = fieldsList.Contains("points") ? u.Points : -1
    })
    .ToList();


        return DalResult<List<User>>.Success(result);
    }

    /// <summary>
    /// 根据用户名和密码获取用户
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>操作结果，包含用户列表或错误消息</returns>
    public static DalResult<List<User>> GetUserByUserNameAndPassword(
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
            return DalResult<List<User>>.Failure("用户名或密码错误");
        }

        var Users = users.Select(user => user).ToList();
        return DalResult<List<User>>.Success(Users);
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
    /// <param name="User">用户模型</param>
    /// <returns>操作结果，包含用户 ID 或错误消息</returns>
    public static DalResult<int> AddUser(User User)
    {
        using var context = DbContextFactory.GetDbContext();
        context.Users.Add(User);
        context.SaveChanges();
        return DalResult<int>.Success(context.Users.Max(user => user.Id));
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="User">用户模型</param>
    /// <returns>操作结果，包含影响的行数或错误消息</returns>
    public static DalResult<int> UpdateUser(User User)
    {
        using var context = DbContextFactory.GetDbContext();
        var userToUpdate = context.Users.Find(User.Id);
        if (userToUpdate == null)
        {
            return DalResult<int>.Failure("用户不存在");
        }

        userToUpdate = User;
        context.Users.Update(userToUpdate);
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
