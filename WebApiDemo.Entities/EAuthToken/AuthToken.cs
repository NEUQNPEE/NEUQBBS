namespace WebApiDemo.Entities.EAuthToken
{
    public class AuthToken
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public required string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
