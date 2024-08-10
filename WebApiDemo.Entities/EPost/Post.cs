namespace WebApiDemo.Entities.EPost;

/// <summary>
/// 帖子模型，包含帖子的基本信息和统计数据。
/// </summary>
public class Post
{
    /// <summary>
    /// 帖子 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户 ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 帖子内容
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; }

    /// <summary>
    /// 点赞数
    /// </summary>
    public int UpVote { get; set; }

    /// <summary>
    /// 踩数
    /// </summary>
    public int DownVote { get; set; }

    /// <summary>
    /// 主帖 ID
    /// </summary>
    public int MainPostId { get; set; }

    /// <summary>
    /// 是否为主帖
    /// </summary>
    public bool IsMainPost { get; set; }

    /// <summary>
    /// 帖子类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 帖子标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewNum { get; set; }

    /// <summary>
    /// 回复次数
    /// </summary>
    public int ReplyNum { get; set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    public bool IsDeleted { get; set; }
}
