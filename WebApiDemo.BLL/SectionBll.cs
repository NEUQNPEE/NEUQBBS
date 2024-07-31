using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.DAL.Interfaces;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.BLL
{
    public class SectionBll : ISectionBll
    {
        private readonly IPostDalFactory _postDalFactory;

        public SectionBll(IPostDalFactory postDalFactory)
        {
            _postDalFactory = postDalFactory;
        }

        public List<Section>? GetAllSections()
        {
            return SectionDal.GetAllSections();
        }

        public List<Post>? GetMainPosts(int sectionId)
        {
            string? tableName = SectionDal.GetSectionById(sectionId)?.TableName;
            if (tableName == null)
            {
                return null;
            }
            return _postDalFactory.GetPostDal(tableName).GetMainPosts();
        }

        public Section? GetSectionById(int id)
        {
            return SectionDal.GetSectionById(id);
        }

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
}
