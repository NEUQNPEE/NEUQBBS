using WebApiDemo.Entities.EUser;

namespace WebApiDemo.Models;

/// <summary>
/// 用户信息响应
/// 与User的差异在于：无密码字段，时间类型字段精度为日
/// </summary>
public class UserInfoResponse
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 注册时间（仅传递年月日）
    /// </summary>
    public string? RegisterTime { get; set; }

    /// <summary>
    /// 最后登录时间（仅传递年月日）
    /// </summary>
    public string? LastLoginTime { get; set; }

    /// <summary>
    /// 用户等级
    /// </summary>
    public int UserLevel { get; set; }

    /// <summary>
    /// 用户积分
    /// </summary>
    public int Points { get; set; }
}

/// <summary>
/// 转换用户模型到用户信息响应
/// </summary>
public static class UserToUserInfoResponse
{
    /// <summary>
    /// 将 <see cref="User"/> 转换为 <see cref="UserInfoResponse"/>
    /// </summary>
    /// <param name="user">用户模型</param>
    /// <returns>用户信息响应</returns>
    public static UserInfoResponse ToResponse(this User user)
    {
        return new UserInfoResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            NickName = user.NickName,
            Gender = user.Gender.ToString(),
            Signature = user.Signature,
            Avatar = user.Avatar,
            RegisterTime = user.RegisterTime.ToString("yyyy-MM-dd"),
            LastLoginTime = user.LastLoginTime.ToString("yyyy-MM-dd"),
            UserLevel = user.UserLevel,
            Points = user.Points
        };
    }

    /// <summary>
    /// 将 <see cref="List{User}"/> 转换为 <see cref="List{UserInfoResponse}"/>
    /// </summary>
    /// <param name="users">用户模型列表</param>
    /// <returns>用户信息响应列表</returns>
    public static List<UserInfoResponse> ToResponse(this List<User> users)
    {
        return users.Select(ToResponse).ToList();
    }
}
