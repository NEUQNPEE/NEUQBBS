using WebApiDemo.Entities.EPost;

namespace WebApiDemo.Models
{
    public class LastReplyInfoResponse
    {
        public int MainPostId { get; set; }
        public required string Username { get; set; }

        public DateTime ReplyTime { get; set; }
    }
}