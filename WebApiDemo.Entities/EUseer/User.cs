namespace WebApiDemo.Entities.EUser
{
    public enum Gender
    {
        Male,
        Female,
        Other,
        PreferNotToSay
    }

    public static class StringToGenderExtensions
    {
        public static Gender ToGender(this string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Gender.PreferNotToSay;
            }
            if (Enum.TryParse(value, true, out Gender gender))
            {
                return gender;
            }
            else
            {
                throw new ArgumentException("Invalid gender value");
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? NickName { get; set; }
        public Gender? Gender { get; set; }
        public string? Signature { get; set; }
        public string? Avatar { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int UserLevel { get; set; }
        public int Points { get; set; }
        public bool IsDeleted { get; set; }
    }
}
