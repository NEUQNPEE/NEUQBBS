using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models;

/// <summary>
/// 用户更新请求
/// </summary>
public class UserUpdateRequest
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

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
}

/// <summary>
/// 转换用户更新请求到用户业务模型
/// </summary>
public static class UserUpdateVToBMapper
{
    /// <summary>
    /// 将 <see cref="UserUpdateRequest"/> 转换为 <see cref="UserBModel"/>
    /// </summary>
    /// <param name="request">用户更新请求</param>
    /// <returns>用户业务模型</returns>
    public static UserBModel ToBModel(this UserUpdateRequest request)
    {
        return new UserBModel
        {
            Id = request.Id,
            Password = request.Password,
            NickName = request.NickName,
            Gender = request.Gender,
            Signature = request.Signature,
            Avatar = request.Avatar
        };
    }
}
