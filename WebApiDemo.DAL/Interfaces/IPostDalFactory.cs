namespace WebApiDemo.DAL.Interfaces;

/// <summary>
/// 帖子数据访问层工厂接口
/// </summary>
public interface IPostDalFactory
{
    /// <summary>
    /// 获取指定表名的帖子数据访问层实例
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>帖子数据访问层实例</returns>
    IPostDal GetPostDal(string tableName);
}
