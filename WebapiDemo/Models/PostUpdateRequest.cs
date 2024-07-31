using WebApiDemo.Entities.EPost;

namespace WebapiDemo.Models
{
    public class PostUpdateRequest
    {
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }
    }
}
