using System.Data;
using WebApiDemo.DAL.Mapper;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EAuthToken;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL;

public class UserDal
{
    public static List<UserBModel> DebugGetAllUsers()
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users.ToList();
        return users
            .Select(user => user?.ToUserBModel())
            .Where(UserBModel => UserBModel != null)
            .ToList()!;
    }

    public static UserBModel? DebugGetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Users.Find(id)?.ToUserBModel();
    }

    public static UserBModel? GetUserById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Users.Find(id)?.ToPublicUserBModel();
    }

    public static List<UserBModel>? GetUserByUserNameAndPassword(string userName, string password)
    {
        using var context = DbContextFactory.GetDbContext();
        var users = context.Users
            .Where(user => user.UserName == userName && user.Password == password)
            .ToList();
        return users.Select(user => user.ToUserBModel()).ToList();
    }

    // 生成一个自动登录Token
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

    // 检查Token登录
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

    public static bool CheckUserExist(string userName)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Users.Any(user => user.UserName == userName);
    }

    public static int AddUser(UserBModel userBModel)
    {
        using var context = DbContextFactory.GetDbContext();
        context.Users.Add(userBModel.ToUser());
        context.SaveChanges();
        return context.Users.Max(user => user.Id);
    }

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

    // 按id设置最后登录时间
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
