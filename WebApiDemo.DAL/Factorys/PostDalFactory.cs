using WebApiDemo.DAL.Interfaces;

namespace WebApiDemo.DAL.Factorys;

/// <summary>
/// 工厂类，用于创建不同类型的帖子数据访问层实例
/// </summary>
public class PostDalFactory : IPostDalFactory
{
    /// <summary>
    /// 根据表名获取相应的帖子数据访问层实例
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>实现 <see cref="IPostDal"/> 接口的实例</returns>
    /// <exception cref="Exception">当表名不匹配任何已知类别时抛出异常</exception>
    public IPostDal GetPostDal(string tableName)
    {
        return tableName switch
        {
            "ComprehensiveSection" => (IPostDal)new ComprehensiveSectionPostDal(),
            _ => throw new Exception("No such category")
        };
    }
}
