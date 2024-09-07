using WebApiDemo.Entities.ESection;
using WebApiDemo.Entities.Factorys;
using WebApiDemo.DAL.Result;

namespace WebApiDemo.DAL;

/// <summary>
/// 版块数据访问层
/// </summary>
public class SectionDal
{
    /// <summary>
    /// 获取所有版块
    /// </summary>
    /// <returns>操作结果，包含版块列表，或在无版块时返回空列表</returns>
    public static DalResult<List<Section>> GetAllSections()
    {
        using var context = DbContextFactory.GetDbContext();
        var sections = context.Sections.ToList();
        return sections.Count != 0
            ? DalResult<List<Section>>.Success(sections)
            : DalResult<List<Section>>.Success([]);
    }

    /// <summary>
    /// 根据 ID 获取版块
    /// </summary>
    /// <param name="id">版块 ID</param>
    /// <returns>操作结果，包含版块信息，或在无匹配版块时返回失败消息</returns>
    public static DalResult<Section> GetSectionById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        var section = context.Sections.Find(id);
        return section != null
            ? DalResult<Section>.Success(section)
            : DalResult<Section>.Failure("未找到指定 ID 的版块");
    }
}
