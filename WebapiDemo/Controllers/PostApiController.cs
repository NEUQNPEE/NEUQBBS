using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebapiDemo.Models;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;

namespace WebapiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("any")]
    public class PostApiController : ControllerBase
    {
        private readonly IPostBll postBll;
        private readonly ILogger<PostApiController> logger;

        public PostApiController(IPostBll postBll, ILogger<PostApiController> logger)
        {
            this.postBll = postBll;
            this.logger = logger;
        }

        [HttpGet("allpost/{sectionId}")]
        public List<Post>? GetAllPostsBySectionId(int sectionId)
        {
            logger.LogDebug("获取板块({sectionId})的所有帖子", sectionId);
            return postBll.GetAllPosts(sectionId);
        }

        [HttpGet("{sectionId}/{mainPostId}")]
        public List<Post>? GetPostsByMainPostId(int sectionId, int mainPostId)
        {
            logger.LogInformation("获取板块({sectionId})的主贴({mainPostId})的所有回复", sectionId, mainPostId);
            return postBll.GetPosts(sectionId, mainPostId);
        }

        [HttpGet("alluser/{sectionId}")]
        public ActionResult<List<UserBaseInfoResponse?>> GetAllUsersBaseInfoBySectionId(int sectionId)
        {
            List<UserBModel>? user = postBll.GetAllUsers(sectionId);
            if (user == null)
            {
                return NotFound("未找到用户基本信息");
            }

            logger.LogInformation("获取在板块({sectionId})中发过帖的所有用户的基本信息", sectionId);
            return Ok(user.Select(u => u.ToUserBaseInfoResponse()));
        }

        [HttpGet("post/{sectionId}")]
        public List<Post>? GetPostsInRangeBySectionId(
            [FromQuery] int sectionId,
            [FromQuery] int beginNum,
            [FromQuery] int needNum
        )
        {
            logger.LogInformation(
                "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子",
                sectionId,
                beginNum,
                needNum
            );
            return postBll.GetPosts(
                new PostListBModel
                {
                    SectionId = sectionId,
                    BeginNum = beginNum,
                    NeedNum = needNum
                }
            );
        }

        [HttpGet("user/{sectionId}")]
        public ActionResult<List<UserBaseInfoResponse?>> GetUsersBaseInfoInRangeBySectionId(
            [FromQuery] int sectionId,
            [FromQuery] int beginNum,
            [FromQuery] int needNum
        )
        {
            List<UserBModel>? user = postBll.GetUsers(
                new PostListBModel
                {
                    SectionId = sectionId,
                    BeginNum = beginNum,
                    NeedNum = needNum
                }
            );

            if (user == null)
            {
                logger.LogWarning(
                    "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表为空！",
                    sectionId,
                    beginNum,
                    needNum
                );
                return NotFound("未找到用户基本信息");
            }

            logger.LogInformation(
                "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表",
                    sectionId,
                    beginNum,
                    needNum
            );
            return Ok(ConvertToUserBaseInfoResponse(user));
        }

        [HttpPost("{sectionId}")]
        public int Insert(int sectionId, [FromBody] PostInsertRequest request)
        {
            logger.LogInformation("用户({userId})在板块({sectionId})发表了帖子", request.UserId, sectionId);
            return postBll.AddPost(sectionId, request.ToPost());
        }

        // 点赞与踩
        [HttpPut("{sectionId}/{postId}")]
        public string Update(int sectionId, int postId, [FromBody] PostUpdateRequest request)
        {
            if (request.UpVote)
            {
                postBll.UpVote(sectionId, postId);
                // todo 需要知道点赞者id
                logger.LogInformation("用户(暂无)在板块({sectionId})的帖子({postId})点赞了", sectionId, postId);
                return "操作成功！";
            }
            else if (request.DownVote)
            {
                postBll.DownVote(sectionId, postId);
                // todo 需要知道踩者id
                logger.LogInformation("用户(暂无)在板块({sectionId})的帖子({postId})点踩了", sectionId, postId);
                return "操作成功！";
            }

            return "操作失败！";
        }

        // 增加浏览量
        [HttpPut("view/{sectionId}/{postId}")]
        public void UpdateView(int sectionId, int postId)
        {
            logger.LogInformation("板块({sectionId})的帖子({postId})被浏览了", sectionId, postId);
            postBll.UpdateView(sectionId, postId);
        }

        private static List<UserBaseInfoResponse> ConvertToUserBaseInfoResponse(List<UserBModel> users)
        {
            return users.Select(u => u.ToUserBaseInfoResponse()).ToList();
        }
    }
}
