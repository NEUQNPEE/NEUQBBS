namespace WebApiDemo.Models;

/// <summary>
/// 最后回复信息
/// </summary>
public class LastReplyInfoResponse
{
    /// <summary>
    /// 回复的帖子的id
    /// </summary>
    public int MainPostId { get; set; }

    /// <summary>
    /// 回复的用户名
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// 最后回复时间
    /// </summary>
    public DateTime ReplyTime { get; set; }
}
