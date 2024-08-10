using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.DAL.Mapper;

/// <summary>
/// 提供将 <see cref="User"/> 映射到 <see cref="UserBModel"/> 的扩展方法
/// </summary>
public static class UserDToBMapper
{
    /// <summary>
    /// 将 <see cref="User"/> 转换为 <see cref="UserBModel"/>
    /// </summary>
    /// <param name="user">源 <see cref="User"/> 实例</param>
    /// <returns>转换后的 <see cref="UserBModel"/> 实例</returns>
    /// <exception cref="ArgumentNullException">当 <paramref name="user"/> 为 null 时抛出</exception>
    public static UserBModel ToUserBModel(this User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        return new UserBModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            NickName = user.NickName,
            Gender = user.Gender.ToString(),
            Signature = user.Signature,
            Avatar = user.Avatar,
            RegisterTime = user.RegisterTime,
            LastLoginTime = user.LastLoginTime,
            UserLevel = user.UserLevel,
            Points = user.Points,
            IsDeleted = user.IsDeleted
        };
    }

    /// <summary>
    /// 将 <see cref="User"/> 转换为仅包含公开属性的 <see cref="UserBModel"/>
    /// </summary>
    /// <param name="user">源 <see cref="User"/> 实例</param>
    /// <returns>转换后的公开属性的 <see cref="UserBModel"/> 实例</returns>
    /// <exception cref="ArgumentNullException">当 <paramref name="user"/> 为 null 时抛出</exception>
    public static UserBModel ToPublicUserBModel(this User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        return new UserBModel
        {
            Id = user.Id,
            UserName = user.UserName,
            NickName = user.NickName,
            Gender = user.Gender.ToString(),
            Signature = user.Signature,
            Avatar = user.Avatar,
            RegisterTime = user.RegisterTime,
            LastLoginTime = user.LastLoginTime,
            UserLevel = user.UserLevel,
            Points = user.Points,
            IsDeleted = user.IsDeleted
        };
    }
}
