using Microsoft.EntityFrameworkCore;
using WebApiDemo.Entities.EUser;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.EAuthToken;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.Entities;

/// <summary>
/// 数据库上下文类，用于与数据库交互
/// </summary>
public class WebApiDemoContext : DbContext
{
    /// <summary>
    /// 用户表
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// 授权令牌表
    /// </summary>
    public DbSet<AuthToken> AuthTokens { get; set; }

    // public DbSet<Web4User> Web4Users { get; set; }

    /// <summary>
    /// 综合版块帖子表
    /// </summary>
    public DbSet<ComprehensiveSectionPost> ComprehensiveSectionPosts { get; set; }

    /// <summary>
    /// 版块表
    /// </summary>
    public DbSet<Section> Sections { get; set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public WebApiDemoContext() { }

    /// <summary>
    /// 带有选项的构造函数
    /// </summary>
    /// <param name="options">数据库上下文选项</param>
    public WebApiDemoContext(DbContextOptions options) : base(options) { }

    /// <summary>
    /// 配置数据库连接
    /// </summary>
    /// <param name="optionsBuilder">数据库上下文选项构建器</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=WebApiDb;User Id=sa;Password=MillionAura69;TrustServerCertificate=True"
        );
    }

    /// <summary>
    /// 配置模型创建行为
    /// </summary>
    /// <param name="modelBuilder">模型构建器</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            // id :自增主键
            entity.HasKey(e => e.Id);

            // UserName :用户名,非空，长度不超过 20 个字符
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(20);

            // Password :密码,非空，在修改为md5 加密后的字符串，长度固定32 个字符
            entity.Property(e => e.Password).IsRequired().HasMaxLength(32);

            // NickName :昵称,长度不超过 20 个字符
            entity.Property(e => e.NickName).HasMaxLength(20);

            // gender :性别，以枚举类型存储,在数据库中指定为字符串类型
            entity.Property(e => e.Gender).HasConversion<string>();

            // signature :个性签名,长度不超过 50 个字符
            entity.Property(e => e.Signature).HasMaxLength(50);

            // avatar :头像,以255 个字符存储头像的 URL
            entity.Property(e => e.Avatar).HasMaxLength(255);

            // registerTime :注册时间，默认为当前时间
            entity.Property(e => e.RegisterTime).HasDefaultValueSql("GETDATE()");

            // lastLoginTime :最后登录时间，默认为当前时间
            entity.Property(e => e.LastLoginTime).HasDefaultValueSql("GETDATE()");

            // userLevel :用户等级,默认为 0
            entity.Property(e => e.UserLevel).HasDefaultValue(0);

            // Points :积分,默认为 0
            entity.Property(e => e.Points).HasDefaultValue(0);

            // IsDelete :是否删除,默认为 false
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        // AuthToken entity configuration
        modelBuilder.Entity<AuthToken>(entity =>
        {
            // TokenId :自增主键
            entity.HasKey(e => e.TokenId);

            // UserId :用户 ID,逻辑上的外键
            entity.Property(e => e.UserId).IsRequired();

            // Token :Token 字符串,长度不超过 255 个字符
            entity.Property(e => e.Token).IsRequired().HasMaxLength(255);

            // ExpireTime :Token 过期时间,非空，默认为当前时间+7 天
            entity
                .Property(e => e.ExpireTime)
                .IsRequired()
                .HasDefaultValueSql("DATEADD(DAY, 7, GETDATE())");
        });

        // modelBuilder.Entity<Web4User>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     entity.Property(e => e.UserName).IsRequired().HasMaxLength(20);
        //     entity.Property(e => e.Password).IsRequired().HasMaxLength(30);
        //     entity.Property(e => e.Address).HasMaxLength(200);
        // });

        modelBuilder.Entity<ComprehensiveSectionPost>(entity =>
        {
            // id :自增主键
            entity.HasKey(e => e.Id);
            // UserId :用户 ID,逻辑上的外键
            entity.Property(e => e.UserId).IsRequired();
            // Content :帖子内容,长度不超过 300 个字符
            entity.Property(e => e.Content).HasMaxLength(300);
            // PublishTime :发布时间,非空，默认为当前时间
            entity.Property(e => e.PublishTime).IsRequired().HasDefaultValueSql("GETDATE()");
            // UpVote :点赞数,默认为 0
            entity.Property(e => e.UpVote).HasDefaultValue(0);
            // DownVote :踩数,默认为 0
            entity.Property(e => e.DownVote).HasDefaultValue(0);
            // MainPostId :主贴 ID,默认为 -1
            entity.Property(e => e.MainPostId).HasDefaultValue(-1);
            // IsMainPost :是否为主贴,默认为 false
            entity.Property(e => e.IsMainPost).HasDefaultValue(false);
            // Type :帖子类型,长度不超过 20 个字符
            entity.Property(e => e.Type).HasMaxLength(20);
            // Title :帖子标题,长度不超过 50 个字符
            entity.Property(e => e.Title).HasMaxLength(50);
            // ViewNum :浏览数,默认为 0
            entity.Property(e => e.ViewNum).HasDefaultValue(0);
            // ReplyNum :回复数,默认为 0
            entity.Property(e => e.ReplyNum).HasDefaultValue(0);
            // IsDeleted :是否删除,默认为 false
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Section>(entity =>
        {
            // id :自增主键
            entity.HasKey(e => e.Id);
            // Area :板块所属区域,长度不超过 20 个字符
            entity.Property(e => e.Area).HasMaxLength(20);
            // Icon :板块图标,长度不超过 255 个字符
            entity.Property(e => e.Icon).HasMaxLength(255);
            // SectionName :板块名称,长度不超过 20 个字符
            entity.Property(e => e.Name).HasMaxLength(20);
            // SectionDescription :板块描述,长度不超过 50 个字符
            entity.Property(e => e.Description).HasMaxLength(50);
            // TableName :板块对应的表名,长度不超过 40 个字符
            entity.Property(e => e.TableName).HasMaxLength(40);
        });
    }
}
