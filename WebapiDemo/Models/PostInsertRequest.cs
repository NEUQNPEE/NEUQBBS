using WebApiDemo.Entities.EPost;

namespace WebApiDemo.Models;

/// <summary>
/// 发帖请求
/// </summary>
public class PostInsertRequest
{
    // UserID,Content,MainPostId,IsMainPost,Type,Title,ViewNum,ReplyNum
    
    /// <summary>
    /// 用户ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 帖子内容，必填项
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// 主帖ID
    /// </summary>
    public int MainPostId { get; set; }

    /// <summary>
    /// 是否为主帖
    /// </summary>
    public bool IsMainPost { get; set; }

    /// <summary>
    /// 帖子的类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 帖子的标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 帖子的浏览次数
    /// </summary>
    public int ViewNum { get; set; }

    /// <summary>
    /// 帖子的回复次数
    /// </summary>
    public int ReplyNum { get; set; }
}

/// <summary>
/// 转换请求到业务模型的扩展方法
/// </summary>
public static class PostVToBMapper
{
    /// <summary>
    /// 将 <see cref="PostInsertRequest"/> 转换为 <see cref="Post"/>
    /// </summary>
    /// <param name="request">发帖请求</param>
    /// <returns>转换后的帖子模型</returns>
    public static Post ToPost(this PostInsertRequest request)
    {
        return new Post
        {
            UserId = request.UserId,
            Content = request.Content,
            MainPostId = request.MainPostId,
            PublishTime = DateTime.Now,
            LastReplyTime = DateTime.Now,
            IsMainPost = request.IsMainPost,
            Type = request.Type,
            Title = request.Title,
            ViewNum = request.ViewNum,
            ReplyNum = request.ReplyNum
        };
    }
}
