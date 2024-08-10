namespace WebApiDemo.Models;

/// <summary>
/// 登录请求
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 记住我
    /// </summary>
    public bool RememberMe { get; set; }

    /// <summary>
    /// 自动登录
    /// </summary>
    public string? AutoLoginToken { get; set; }
}
