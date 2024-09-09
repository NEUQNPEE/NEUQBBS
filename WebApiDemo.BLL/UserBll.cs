using WebApiDemo.BLL.Interfaces;
using WebApiDemo.BLL.Result;
using WebApiDemo.DAL;
using WebApiDemo.Entities.BModels;

using WebApiDemoCommon;

namespace WebApiDemo.BLL;

/// <summary>
/// 用户业务逻辑实现
/// </summary>
public class UserBll : IUserBll
{
    /// <inheritdoc />
    public BllResult<List<UserBModel>> DebugGetAllUsers()
    {
        var users = UserDal.DebugGetAllUsers().Data;
        return users != null 
            ? BllResult<List<UserBModel>>.Success(users) 
            : BllResult<List<UserBModel>>.Failure();
    }

    /// <inheritdoc />
    public BllResult<UserBModel> DebugGetUserById(int id)
    {
        var user = UserDal.DebugGetUserById(id).Data;
        return user != null 
            ? BllResult<UserBModel>.Success(user) 
            : BllResult<UserBModel>.Failure();
    }

    /// <inheritdoc />
    public BllResult<UserBModel> GetUserById(int id)
    {
        var user = UserDal.GetUserById(id).Data;
        return user != null 
            ? BllResult<UserBModel>.Success(user) 
            : BllResult<UserBModel>.Failure();
    }

    /// <inheritdoc />
    public BllResult<string> GetUserNameById(int id)
    {
        var user = UserDal.GetUserById(id).Data;
        return user != null 
            ? BllResult<string>.Success(user.UserName) 
            : BllResult<string>.Failure("未找到");
    }

    /// <inheritdoc />
    public BllResult<List<string>> GetUserNamesByIds(List<int> ids)
    {
        List<string> userNames = [];
        foreach (int id in ids)
        {
            var result = GetUserNameById(id);
            if (result.IsSuccess)
            {
                userNames.Add(result.Data!);
            }
        }
        return userNames.Count > 0 
            ? BllResult<List<string>>.Success(userNames) 
            : BllResult<List<string>>.Failure("未找到");
    }

    /// <inheritdoc />
    public BllResult<UserBModel> GetUserByIdForPost(int id)
    {
        var user = UserDal.GetUserById(id).Data;
        if (user == null)
        {
            return BllResult<UserBModel>.Failure("未找到");
        }

        return BllResult<UserBModel>.Success(new UserBModel
        {
            UserName = user.UserName,
            RegisterTime = user.RegisterTime,
            Points = user.Points
        });
    }

    /// <inheritdoc />
    public BllResult<UserBModel> GetUserBaseInfoById(int id)
    {
        var user = UserDal.GetUserById(id).Data;
        if (user == null)
        {
            return BllResult<UserBModel>.Failure("未找到");
        }

        return BllResult<UserBModel>.Success(new UserBModel
        {
            Id = user.Id,
            UserName = user.UserName,
            RegisterTime = user.RegisterTime,
            Points = user.Points
        });
    }

    /// <inheritdoc />
    public BllResult<UserBModel> GetUserDetailInfoById(int id)
    {
        var user = UserDal.GetUserById(id).Data;
        return user != null 
            ? BllResult<UserBModel>.Success(user) 
            : BllResult<UserBModel>.Failure("未找到");
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
    public BllResult<int> AddUser(UserBModel userBModel)
    {
        if (userBModel.Password == null)
        {
            return BllResult<int>.Failure("密码为空!");
        }

        userBModel.Password = userBModel.Password.ToMd5();
        int userId = UserDal.AddUser(userBModel).Data;
        return BllResult<int>.Success(userId);
    }

    /// <inheritdoc />
    public BllResult<string> UpdateUser(UserBModel userBModel)
    {
        if (userBModel.Password != null)
        {
            userBModel.Password = userBModel.Password.ToMd5();
        }

        int result = UserDal.UpdateUser(userBModel).Data;
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
