using WebApiDemo.BLL.Interfaces;
using WebApiDemo.BLL.Result;
using WebApiDemo.DAL;
using WebApiDemo.DAL.Interfaces;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.BLL;

/// <summary>
/// 版块业务逻辑实现
/// </summary>
/// <inheritdoc />
public class SectionBll(IPostDalFactory postDalFactory) : ISectionBll
{
    /// <inheritdoc />
    public BllResult<List<Section>> GetAllSections()
    {
        var sections = SectionDal.GetAllSections().Data;
        return sections != null 
            ? BllResult<List<Section>>.Success(sections) 
            : BllResult<List<Section>>.Failure("未找到版块");
    }

    /// <inheritdoc />
    public BllResult<List<Post>> GetMainPosts(int sectionId)
    {
        string? tableName = SectionDal.GetSectionById(sectionId).Data?.TableName;
        if (tableName == null)
        {
            return BllResult<List<Post>>.Failure("未找到板块");
        }
        var posts = postDalFactory.GetPostDal(tableName).GetMainPosts().Data;
        return posts != null 
            ? BllResult<List<Post>>.Success(posts) 
            : BllResult<List<Post>>.Failure("未找到板块");
    }

    /// <inheritdoc />
    public BllResult<Section> GetSectionById(int id)
    {
        var section = SectionDal.GetSectionById(id).Data;
        return section != null 
            ? BllResult<Section>.Success(section) 
            : BllResult<Section>.Failure("未找到板块");
    }

    /// <inheritdoc />
    public BllResult<List<Section>> GetSectionsByIds(List<int> ids)
    {
        List<Section> sections = [];
        foreach (int id in ids)
        {
            var section = SectionDal.GetSectionById(id).Data;
            if (section != null)
            {
                sections.Add(section);
            }
        }
        return sections.Count > 0 
            ? BllResult<List<Section>>.Success(sections) 
            : BllResult<List<Section>>.Failure("未找到板块");
    }
}
