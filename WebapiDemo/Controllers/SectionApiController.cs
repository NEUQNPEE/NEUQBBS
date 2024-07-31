using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebapiDemo.Models;

using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebapiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("any")]
    public class SectionApiController : ControllerBase
    {
        private readonly ISectionBll sectionBll;
        private readonly IUserBll userBll;

        private readonly IPostBll postBll;

        public SectionApiController(ISectionBll sectionBll, IUserBll userBll, IPostBll postBll)
        {
            this.sectionBll = sectionBll;
            this.userBll = userBll;
            this.postBll = postBll;
        }

        [HttpGet("all")]
        public List<Section>? GetAll()
        {
            return sectionBll.GetAllSections();
        }

        // 按sectionId获取Section
        [HttpGet("{id}")]
        public Section? GetById(int id)
        {
            return sectionBll.GetSectionById(id);
        }

        // 按sectionId获取本版所有主贴
        [HttpGet("mainposts/{sectionId}")]
        public List<Post>? GetMainPosts(int sectionId)
        {
            return sectionBll.GetMainPosts(sectionId);
        }

        // 前端传来List<int> mainPostIds,获取所有主贴的最后回复信息
        [HttpPost("lastreplyinfo/{sectionId}")]
        public List<LastReplyInfoResponse>? GetLastReplyInfo(int sectionId,[FromBody] List<int> mainPostIds)
        {
            // 获取所有主贴的最后回复帖
            List<Post> lastReplyPosts = new();
            foreach (int mainPostId in mainPostIds)
            {
                Post? lastReplyPost = postBll.GetLastReplyPosts(sectionId, mainPostId);
                if (lastReplyPost != null)
                {
                    lastReplyPosts.Add(lastReplyPost);
                }
            }

            List<LastReplyInfoResponse> lastReplyInfoResponses = new();
            for (int i = 0; i < lastReplyPosts.Count; i++)
            {
                // 从回复帖中获取主贴id和发表时间，根据用户id获取用户名
                lastReplyInfoResponses.Add(new LastReplyInfoResponse
                {
                    MainPostId = lastReplyPosts[i].MainPostId,
                    Username = userBll.GetUserNameById(lastReplyPosts[i].UserId),
                    ReplyTime = lastReplyPosts[i].PublishTime
                });
            }

            return lastReplyInfoResponses;
        }
    }
}
