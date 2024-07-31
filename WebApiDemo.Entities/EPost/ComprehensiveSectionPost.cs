using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Entities.EPost
{
    public class ComprehensiveSectionPost : Post { }

    public static class ToComprehensiveSectionPostMapper
    {
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
}
