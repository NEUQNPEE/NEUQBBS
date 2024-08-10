using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.DAL.Interfaces;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.BLL;

/// <summary>
/// 版块业务逻辑实现
/// </summary>
public class SectionBll : ISectionBll
{
    private readonly IPostDalFactory _postDalFactory;

    /// <inheritdoc />
    public SectionBll(IPostDalFactory postDalFactory)
    {
        _postDalFactory = postDalFactory;
    }

    /// <inheritdoc />
    public List<Section>? GetAllSections()
    {
        return SectionDal.GetAllSections();
    }

    /// <inheritdoc />
    public List<Post>? GetMainPosts(int sectionId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
        if (tableName == null)
        {
            return null;
        }
        return _postDalFactory.GetPostDal(tableName).GetMainPosts();
    }

    /// <inheritdoc />
    public Section? GetSectionById(int id)
    {
        return SectionDal.GetSectionById(id);
    }

    /// <inheritdoc />
    public List<Section>? GetSectionsByIds(List<int> ids)
    {
        List<Section> sections = new();
        foreach (int id in ids)
        {
            Section? section = GetSectionById(id);
            if (section != null)
            {
                sections.Add(section);
            }
        }
        return sections;
    }
}
