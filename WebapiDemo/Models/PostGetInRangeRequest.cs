using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models
{
    public class PostGetInRangeRequest
    {
        public int BeginNum { get; set; }
        public int NeedNum { get; set; }
    }

    
    // Topostlist
    public static class PostGetInRangeRequestVToBPostList
    {
        public static PostListBModel ToPostListBModel(this PostGetInRangeRequest request, int sectionId)
        {
            return new PostListBModel
            {
                SectionId = 0,
                BeginNum = request.BeginNum,
                NeedNum = request.NeedNum
            };
        }
    }

}