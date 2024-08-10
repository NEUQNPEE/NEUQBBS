namespace WebApiDemo.Entities.EAuthToken;

/// <summary>
/// 自动登录令牌模型
/// </summary>
public class AuthToken
{
    /// <summary>
    /// 令牌 ID
    /// </summary>
    public int TokenId { get; set; }

    /// <summary>
    /// 用户 ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 令牌字符串
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// 令牌过期时间
    /// </summary>
    public DateTime ExpireTime { get; set; }
}
