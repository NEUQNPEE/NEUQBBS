namespace WebApiDemo.Entities.EUser;

/// <summary>
/// 用户性别枚举
/// </summary>
public enum Gender
{
    /// <summary>
    /// 男性
    /// </summary>
    Male,

    /// <summary>
    /// 女性
    /// </summary>
    Female,

    /// <summary>
    /// 其他
    /// </summary>
    Other,

    /// <summary>
    /// 不愿透露
    /// </summary>
    PreferNotToSay
}

/// <summary>
/// 字符串到性别枚举的扩展方法
/// </summary>
public static class StringToGenderExtensions
{
    /// <summary>
    /// 将字符串转换为 <see cref="Gender"/> 枚举
    /// </summary>
    /// <param name="value">要转换的字符串</param>
    /// <returns>转换后的性别枚举，如果字符串为空或无效，则返回 <see cref="Gender.PreferNotToSay"/></returns>
    /// <exception cref="ArgumentException">如果字符串无效</exception>
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

/// <summary>
/// 用户模型，包含用户的基本信息和统计数据
/// </summary>
public class User
{
    /// <summary>
    /// 用户 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 注册时间
    /// </summary>
    public DateTime RegisterTime { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime LastLoginTime { get; set; }

    /// <summary>
    /// 用户等级
    /// </summary>
    public int UserLevel { get; set; }

    /// <summary>
    /// 用户积分
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    public bool IsDeleted { get; set; }
}
