using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.Entities.BModels;
using WebApiDemoCommon;

namespace WebApiDemo.BLL;

/// <summary>
/// 用户业务逻辑接口实现
/// </summary>
public class UserBll : IUserBll
{
    /// <inheritdoc />
    public List<UserBModel> DebugGetAllUsers()
    {
        return UserDal.DebugGetAllUsers();
    }

    /// <inheritdoc />
    public UserBModel? DebugGetUserById(int id)
    {
        return UserDal.DebugGetUserById(id);
    }

    /// <inheritdoc />
    public UserBModel? GetUserById(int id)
    {
        return UserDal.GetUserById(id);
    }

    /// <inheritdoc />
    public string GetUserNameById(int id)
    {
        UserBModel? user = UserDal.GetUserById(id);
        if (user == null)
        {
            return "未找到";
        }

        return user.UserName ?? "用户名空引用！";
    }

    /// <inheritdoc />
    public List<string> GetUserNamesByIds(List<int> ids)
    {
        List<string> userNames = new();
        foreach (int id in ids)
        {
            userNames.Add(GetUserNameById(id));
        }

        return userNames;
    }

    /// <inheritdoc />
    public UserBModel? GetUserByIdForPost(int id)
    {
        UserBModel? user = UserDal.GetUserById(id);
        if (user == null)
        {
            return null;
        }

        return new UserBModel
        {
            UserName = user.UserName,
            RegisterTime = user.RegisterTime,
            Points = user.Points
        };
    }

    /// <inheritdoc />
    public UserBModel? GetUserBaseInfoById(int id)
    {
        UserBModel? user = UserDal.GetUserById(id);
        if (user == null)
        {
            return null;
        }

        return new UserBModel
        {
            Id = user.Id,
            UserName = user.UserName,
            RegisterTime = user.RegisterTime,
            Points = user.Points
        };
    }

    /// <inheritdoc />
    public UserBModel? GetUserDetailInfoById(int id)
    {
        return UserDal.GetUserById(id);
    }

    /// <inheritdoc />
    public bool CheckUserExist(string userName)
    {
        return UserDal.CheckUserExist(userName);
    }

    /// <inheritdoc />
    public int CheckLogin(string userName, string password)
    {
        password = password.ToMd5();
        List<UserBModel>? userList = UserDal.GetUserByUserNameAndPassword(userName, password);
        if (userList?.Count <= 0 || userList == null)
        {
            return -1;
        }

        // 查出非删除的用户
        UserBModel? user = userList.Find(user => user?.IsDeleted == false);
        if (user == null)
        {
            return -1;
        }

        UserDal.UpdateLastLoginTime(user.Id);
        return user.Id;
    }

    /// <inheritdoc />
    public string GenerateAutoLoginToken(int userId)
    {
        return UserDal.GenerateAutoLoginToken(userId);
    }

    /// <inheritdoc />
    public int CheckAutoLoginToken(string token)
    {
        int userId = UserDal.CheckAutoLoginToken(token);
        if (userId > 0)
        {
            UserDal.UpdateLastLoginTime(userId);
        }
        return userId;
    }

    /// <inheritdoc />
    public int AddUser(UserBModel userBModel)
    {
        if (userBModel.Password == null)
        {
            return -1;
        }
        // 将密码转为md5
        userBModel.Password = userBModel.Password.ToMd5();
        return UserDal.AddUser(userBModel);
    }

    /// <inheritdoc />
    public string UpdateUser(UserBModel userBModel)
    {
        if (userBModel.Password != null)
        {
            userBModel.Password = userBModel.Password.ToMd5();
        }
        return UserDal.UpdateUser(userBModel) > 0 ? "更新成功" : "更新失败";
    }

    /// <inheritdoc />
    public string RemoveUser(int id)
    {
        return UserDal.RemoveUser(id) > 0 ? "删除成功" : "删除失败";
    }
}
