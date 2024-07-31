namespace WebApiDemo.Entities.BModels
{
    public class UserBModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? NickName { get; set; }
        public string? Gender { get; set; }
        public string? Signature { get; set; }
        public string? Avatar { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int UserLevel { get; set; }
        public int Points { get; set; }
        public bool IsDeleted { get; set; }
    }
}
