using WebApiDemo.BLL.Result;
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
    /// <returns>包含版块列表的操作结果，或在无版块时返回空列表。</returns>
    BllResult<List<Section>> GetAllSections();

    /// <summary>
    /// 根据 ID 获取版块信息
    /// </summary>
    /// <param name="id">版块 ID</param>
    /// <returns>包含版块信息的操作结果，或在无匹配版块时返回 null。</returns>
    BllResult<Section> GetSectionById(int id);

    /// <summary>
    /// 根据 ID 列表获取版块信息
    /// </summary>
    /// <param name="ids">版块 ID 列表</param>
    /// <returns>包含版块信息列表的操作结果，或在无匹配版块时返回空列表。</returns>
    BllResult<List<Section>> GetSectionsByIds(List<int> ids);

    /// <summary>
    /// 获取指定版块的主帖
    /// </summary>
    /// <param name="sectionId">版块 ID</param>
    /// <returns>包含主帖列表的操作结果，或在无主帖时返回空列表。</returns>
    BllResult<List<Post>> GetMainPosts(int sectionId);
}
