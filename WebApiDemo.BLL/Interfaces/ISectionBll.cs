using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.BLL.Interfaces
{
    public interface ISectionBll
    {
        List<Section>? GetAllSections();

        Section? GetSectionById(int id);

        List<Section>? GetSectionsByIds(List<int> ids);

        List<Post>? GetMainPosts(int sectionId);
    }
}