using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.BLL.Interfaces;

/// <summary>
/// 版块业务逻辑接口
/// </summary>
public interface ISectionBll
{
    /// <summary>
    /// 获取所有版块信息
    /// </summary>
    /// <returns>版块列表，或在无版块时返回空列表</returns>
    List<Section>? GetAllSections();

    /// <summary>
    /// 根据 ID 获取版块信息
    /// </summary>
    /// <param name="id">版块 ID</param>
    /// <returns>版块信息，或在无匹配版块时返回 null</returns>
    Section? GetSectionById(int id);

    /// <summary>
    /// 根据 ID 列表获取版块信息
    /// </summary>
    /// <param name="ids">版块 ID 列表</param>
    /// <returns>版块信息列表，或在无匹配版块时返回空列表</returns>
    List<Section>? GetSectionsByIds(List<int> ids);

    /// <summary>
    /// 获取指定版块的主帖
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <returns>主帖列表，或在无主帖时返回空列表</returns>
    List<Post>? GetMainPosts(int sectionId);
}
