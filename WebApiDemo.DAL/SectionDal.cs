using WebApiDemo.Entities.ESection;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL;

/// <summary>
/// 版块数据访问层
/// </summary>
public class SectionDal
{
    /// <summary>
    /// 获取所有版块
    /// </summary>
    /// <returns>版块列表，或在无版块时返回空列表</returns>
    public static List<Section>? GetAllSections()
    {
        using var context = DbContextFactory.GetDbContext();
        var sections = context.Sections.ToList();
        if (sections == null)
        {
            return null;
        }
        return sections;
    }

    /// <summary>
    /// 根据 ID 获取版块
    /// </summary>
    /// <param name="id">版块 ID</param>
    /// <returns>版块信息，或在无匹配版块时返回 null</returns>
    public static Section? GetSectionById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Sections.Find(id);
    }
}
