namespace WebApiDemo.Entities.EPost;

/// <summary>
/// 表示综合版块帖子，继承自 <see cref="Post"/> 类。
/// </summary>
public class ComprehensiveSectionPost : Post { }

/// <summary>
/// 扩展方法，用于将 <see cref="Post"/> 转换为 <see cref="ComprehensiveSectionPost"/>。
/// </summary>
public static class ToComprehensiveSectionPostMapper
{
    /// <summary>
    /// 将 <see cref="Post"/> 转换为 <see cref="ComprehensiveSectionPost"/>。
    /// </summary>
    /// <param name="post">要转换的帖子</param>
    /// <returns>转换后的综合版块帖子</returns>
    public static ComprehensiveSectionPost ToComprehensiveSectionPost(this Post post)
    {
        return new ComprehensiveSectionPost
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            PublishTime = post.PublishTime,
            UpVote = post.UpVote,
            DownVote = post.DownVote,
            MainPostId = post.MainPostId,
            IsMainPost = post.IsMainPost,
            Type = post.Type,
            Title = post.Title,
            ViewNum = post.ViewNum,
            ReplyNum = post.ReplyNum,
            IsDeleted = post.IsDeleted
        };
    }
}
