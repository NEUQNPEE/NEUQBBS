using WebApiDemo.BLL.Interfaces;
using WebApiDemo.BLL.Result;
using WebApiDemo.DAL;
using WebApiDemo.Entities.EUser;
using WebApiDemoCommon;

namespace WebApiDemo.BLL;

/// <summary>
/// 用户业务逻辑实现
/// </summary>
public class UserBll : IUserBll
{
    /// <inheritdoc />
    public BllResult<List<User>> DebugGetAllUsers()
    {
        var users = UserDal.DebugGetAllUsers().Data;
        return users != null 
            ? BllResult<List<User>>.Success(users) 
            : BllResult<List<User>>.Failure();
    }

    /// <inheritdoc />
    public BllResult<User> DebugGetUserById(int id)
    {
        var user = UserDal.DebugGetUserById(id).Data;
        return user != null 
            ? BllResult<User>.Success(user) 
            : BllResult<User>.Failure();
    }

    /// <inheritdoc />
    public BllResult<User> GetUserInfoById(int id, string fields)
    {
        var user = UserDal.GetUserInfoById(id, fields).Data;
        return user != null
            ? BllResult<User>.Success(user)
            : BllResult<User>.Failure();
    }

    /// <inheritdoc />
    public BllResult<List<User>> GetUserInfoById(IEnumerable<int> ids, string fields)
    {
        var users = UserDal.GetUserInfoById(ids, fields).Data;
        return users != null
            ? BllResult<List<User>>.Success(users)
            : BllResult<List<User>>.Success([]);
    }

    /// <inheritdoc />
    public BllResult<bool> CheckUserExist(string userName)
    {
        bool exists = UserDal.CheckUserExist(userName).IsSuccess;
        return exists
            ? BllResult<bool>.Success(exists)
            : BllResult<bool>.Failure("未找到");
    }

    /// <inheritdoc />
    public BllResult<int> CheckLogin(string userName, string password)
    {
        password = password.ToMd5();
        var userList = UserDal.GetUserByUserNameAndPassword(userName, password).Data;
        if (userList?.Count <= 0 || userList == null)
        {
            return BllResult<int>.Failure("用户名或密码错误");
        }

        var user = userList.Find(u => u?.IsDeleted == false);
        if (user == null)
        {
            return BllResult<int>.Failure("用户名或密码错误");
        }

        UserDal.UpdateLastLoginTime(user.Id);
        return BllResult<int>.Success(user.Id);
    }

    /// <inheritdoc />
    public BllResult<string> GenerateAutoLoginToken(int userId)
    {
        var result = UserDal.GenerateAutoLoginToken(userId);
        if (!result.IsSuccess)
        {
            return BllResult<string>.Failure();
        }
        return BllResult<string>.Success(result.Data);
    }

    /// <inheritdoc />
    public BllResult<int> CheckAutoLoginToken(string token)
    {
        var result = UserDal.CheckAutoLoginToken(token);
        if (!result.IsSuccess)
        {
            return BllResult<int>.Failure(result.Message);
        }
            
        UserDal.UpdateLastLoginTime(result.Data);
        return BllResult<int>.Success(result.Data);
    }

    /// <inheritdoc />
    public BllResult<int> AddUser(User User)
    {
        if (User.Password == null)
        {
            return BllResult<int>.Failure("密码为空!");
        }

        User.Password = User.Password.ToMd5();
        int userId = UserDal.AddUser(User).Data;
        return BllResult<int>.Success(userId);
    }

    /// <inheritdoc />
    public BllResult<string> UpdateUser(User User)
    {
        if (User.Password != null)
        {
            User.Password = User.Password.ToMd5();
        }

        int result = UserDal.UpdateUser(User).Data;
        return result > 0 
            ? BllResult<string>.Success("更新成功") 
            : BllResult<string>.Failure("更新失败");
    }

    /// <inheritdoc />
    public BllResult<string> RemoveUser(int id)
    {
        int result = UserDal.RemoveUser(id).Data;
        return result > 0 
            ? BllResult<string>.Success("删除成功") 
            : BllResult<string>.Failure("删除失败");
    }
}
