using WebApiDemo.Entities.EUser;

namespace WebApiDemo.Models;

/// <summary>
/// 用户插入请求
/// </summary>
public class UserInsertRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public required string Password { get; set; }

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
/// 转换用户插入请求到用户模型
/// </summary>
public static class UserInsertRequestExtensions
{
    /// <summary>
    /// 将 <see cref="UserInsertRequest"/> 转换为 <see cref="User"/>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static User ToUser(this UserInsertRequest request)
    {
        return new User
        {
            UserName = request.UserName,
            Password = request.Password,
            NickName = request.NickName,
            Gender = request.Gender.ToGender(),
            Signature = request.Signature,
            Avatar = request.Avatar
        };
    }
}
