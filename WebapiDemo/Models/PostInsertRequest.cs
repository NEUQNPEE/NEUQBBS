using WebApiDemo.Entities.EPost;

namespace WebapiDemo.Models
{
    public class PostInsertRequest
    {
        // UserID,Content,MainPostId,IsMainPost,Type,Title,ViewNum,ReplyNum
        public int UserId { get; set; }
        public required string Content { get; set; }
        public int MainPostId { get; set; }
        public bool IsMainPost { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int ViewNum { get; set; }
        public int ReplyNum { get; set; }
    }

    public static class PostVToBMapper
    {
        public static Post ToPost(this PostInsertRequest request)
        {
            return new Post
            {
                UserId = request.UserId,
                Content = request.Content,
                MainPostId = request.MainPostId,
                IsMainPost = request.IsMainPost,
                Type = request.Type,
                Title = request.Title,
                ViewNum = request.ViewNum,
                ReplyNum = request.ReplyNum
            };
        }
    }
}