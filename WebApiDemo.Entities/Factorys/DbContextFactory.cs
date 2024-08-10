using Microsoft.EntityFrameworkCore;

namespace WebApiDemo.Entities.Factorys;

/// <summary>
/// 数据上下文工厂，用于创建 <see cref="WebApiDemoContext"/> 实例
/// </summary>
public class DbContextFactory
{
    /// <summary>
    /// 获取一个新的 <see cref="WebApiDemoContext"/> 实例
    /// </summary>
    /// <returns>配置为不跟踪查询的 <see cref="WebApiDemoContext"/> 实例</returns>
    public static WebApiDemoContext GetDbContext()
    {
        var dbContext = new WebApiDemoContext();
        dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        return dbContext;
    }
}
