using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models;

/// <summary>
/// 用户基本信息响应
/// </summary>
public class UserBaseInfoResponse
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
    /// 注册时间（仅传递年月日）
    /// </summary>
    public required string RegisterTime { get; set; }

    /// <summary>
    /// 用户积分
    /// </summary>
    public int Points { get; set; }
}

/// <summary>
/// 转换用户业务模型到用户基本信息响应
/// </summary>
public static class UserBToVUserBaseInfoResponseMapper
{
    /// <summary>
    /// 将 <see cref="UserBModel"/> 转换为 <see cref="UserBaseInfoResponse"/>
    /// </summary>
    /// <param name="u">用户业务模型</param>
    /// <returns>用户基本信息响应</returns>
    public static UserBaseInfoResponse ToUserBaseInfoResponse(this UserBModel u)
    {
        return new UserBaseInfoResponse
        {
            Id = u.Id,
            UserName = u.UserName ?? "用户名空引用！",
            RegisterTime = u.RegisterTime.ToString("yyyy-MM-dd"),
            Points = u.Points
        };
    }
}
