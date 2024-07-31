namespace WebApiDemo.Entities.EPost
{
    public class Post
    {
        // 包括Id,Type,Content,PublishTime,UserId,UpVote,DownVote,IsDeleted,MainPostId,IsMainPost,Title,ViewNum,ReplyNum
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Content { get; set; }
        public DateTime PublishTime { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int MainPostId { get; set; }
        public bool IsMainPost { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int ViewNum { get; set; }
        public int ReplyNum { get; set; }
        public bool IsDeleted { get; set; }
    }
}