using System.Data;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;
using WebApiDemo.Entities.Factorys;

namespace WebApiDemo.DAL;

public class SectionDal
{
    // 获取所有Section
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

    // 根据Id获取Section
    public static Section? GetSectionById(int id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Sections.Find(id);
    }
}

