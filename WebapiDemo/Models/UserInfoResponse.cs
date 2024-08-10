using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models;

/// <summary>
/// 用户信息响应
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
    public required string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public required string NickName { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public required string Gender { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public required string Signature { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public required string Avatar { get; set; }

    /// <summary>
    /// 注册时间（仅传递年月日）
    /// </summary>
    public required string RegisterTime { get; set; }

    /// <summary>
    /// 最后登录时间（仅传递年月日）
    /// </summary>
    public required string LastLoginTime { get; set; }

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
/// 转换用户业务模型到用户信息响应
/// </summary>
public static class UserBToVUserInfoResponseMapper
{
    /// <summary>
    /// 将 <see cref="UserBModel"/> 转换为 <see cref="UserInfoResponse"/>
    /// </summary>
    /// <param name="userInfoBModel">用户业务模型</param>
    /// <returns>用户信息响应</returns>
    public static UserInfoResponse ToResponse(this UserBModel userInfoBModel)
    {
        return new UserInfoResponse
        {
            Id = userInfoBModel.Id,
            UserName = userInfoBModel.UserName ?? "用户名空引用！",
            NickName = userInfoBModel.NickName ?? "昵称空引用！",
            Gender = userInfoBModel.Gender ?? "性别空引用！",
            Signature = userInfoBModel.Signature ?? "签名空引用！",
            Avatar = userInfoBModel.Avatar ?? "头像空引用！",
            RegisterTime = userInfoBModel.RegisterTime.ToString("yyyy-MM-dd"),
            LastLoginTime = userInfoBModel.LastLoginTime.ToString("yyyy-MM-dd"),
            UserLevel = userInfoBModel.UserLevel,
            Points = userInfoBModel.Points
        };
    }
}
