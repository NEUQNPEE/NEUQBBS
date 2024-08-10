namespace WebApiDemo.Models;

/// <summary>
/// 帖子更新请求
/// </summary>
public class PostUpdateRequest
{
    /// <summary>
    /// 点赞
    /// </summary>
    public bool UpVote { get; set; }

    /// <summary>
    /// 点踩
    /// </summary>
    public bool DownVote { get; set; }
}
