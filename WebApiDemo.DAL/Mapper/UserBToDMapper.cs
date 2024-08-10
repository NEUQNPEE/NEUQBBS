using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.DAL.Mapper;

/// <summary>
/// 提供将 <see cref="UserBModel"/> 映射到 <see cref="User"/> 的扩展方法
/// </summary>
public static class UserBToDMapper
{
    /// <summary>
    /// 对 <see cref="User"/> 提供一个扩展方法，将 <see cref="UserBModel"/> 中每一个非空属性赋值给 <see cref="User"/>
    /// </summary>
    /// <param name="user">目标 <see cref="User"/> 实例</param>
    /// <param name="userBModel">源 <see cref="UserBModel"/> 实例</param>
    /// <returns>更新后的 <see cref="User"/> 实例</returns>
    public static User UpdateFromBModel(this User user, UserBModel userBModel)
    {
        user.Id = userBModel.Id;

        if (userBModel.UserName != null)
        {
            user.UserName = userBModel.UserName;
        }

        if (userBModel.Password != null)
        {
            user.Password = userBModel.Password;
        }

        if (userBModel.NickName != null)
        {
            user.NickName = userBModel.NickName;
        }

        if (userBModel.Gender != null)
        {
            user.Gender = Enum.TryParse<Gender>(userBModel.Gender, out var parsedGender)
                ? parsedGender
                : null;
        }

        if (userBModel.Signature != null)
        {
            user.Signature = userBModel.Signature;
        }

        if (userBModel.Avatar != null)
        {
            user.Avatar = userBModel.Avatar;
        }

        user.LastLoginTime = userBModel.LastLoginTime;

        user.UserLevel = userBModel.UserLevel;

        user.Points = userBModel.Points;

        user.IsDeleted = userBModel.IsDeleted;

        return user;
    }

    /// <summary>
    /// 将 <see cref="UserBModel"/> 转换为 <see cref="User"/>
    /// </summary>
    /// <param name="userBModel">源 <see cref="UserBModel"/> 实例</param>
    /// <returns>转换后的 <see cref="User"/> 实例</returns>
    public static User ToUser(this UserBModel userBModel)
    {
        return new User
        {
            Id = userBModel.Id,
            UserName = userBModel.UserName ?? string.Empty,
            Password = userBModel.Password ?? string.Empty,
            NickName = userBModel.NickName,
            Gender = Enum.TryParse<Gender>(userBModel.Gender, out var parsedGender)
                ? parsedGender
                : null,
            Signature = userBModel.Signature,
            Avatar = userBModel.Avatar,
            LastLoginTime = userBModel.LastLoginTime,
            UserLevel = userBModel.UserLevel,
            Points = userBModel.Points,
            IsDeleted = userBModel.IsDeleted
        };
    }
}
