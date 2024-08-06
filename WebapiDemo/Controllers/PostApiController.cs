using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebapiDemo.Models;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL.Interfaces;
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

        /// <summary>
        /// 获取板块({sectionId})的所有帖子
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpGet("allpost/{sectionId}")]
        public ActionResult<List<Post>> GetAllPostsBySectionId(int sectionId)
        {
            logger.LogDebug("获取板块({sectionId})的所有帖子", sectionId);
            var posts = postBll.GetAllPosts(sectionId);
            if (posts == null || posts.Count == 0)
            {
                return NotFound($"未找到板块ID为 {sectionId} 的帖子");
            }
            return Ok(posts);
        }

        /// <summary>
        /// 获取板块({sectionId})的主贴({mainPostId})的所有回复
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="mainPostId"></param>
        /// <returns></returns>
        [HttpGet("{sectionId}/{mainPostId}")]
        public ActionResult<List<Post>> GetPostsByMainPostId(int sectionId, int mainPostId)
        {
            logger.LogInformation("获取板块({sectionId})的主贴({mainPostId})的所有回复", sectionId, mainPostId);
            var posts = postBll.GetPosts(sectionId, mainPostId);
            if (posts == null || posts.Count == 0)
            {
                return NotFound($"未找到板块ID为 {sectionId} 的主贴ID为 {mainPostId} 的回复");
            }
            return Ok(posts);
        }

        /// <summary>
        /// 获取在板块({sectionId})中发过帖的所有用户
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpGet("alluser/{sectionId}")]
        public ActionResult<IEnumerable<UserBaseInfoResponse>> GetAllUsersBaseInfoBySectionId(
            int sectionId
        )
        {
            List<UserBModel>? users = postBll.GetAllUsers(sectionId);
            if (users == null || users.Count == 0)
            {
                return NotFound("未找到用户基本信息");
            }

            logger.LogInformation("获取在板块({sectionId})中发过帖的所有用户的基本信息", sectionId);
            return Ok(users.Select(u => u.ToUserBaseInfoResponse()));
        }

        /// <summary>
        /// 获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="beginNum"></param>
        /// <param name="needNum"></param>
        /// <returns></returns>
        [HttpGet("post/{sectionId}")]
        public ActionResult<List<Post>> GetPostsInRangeBySectionId(
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
            var posts = postBll.GetPosts(
                new PostListBModel
                {
                    SectionId = sectionId,
                    BeginNum = beginNum,
                    NeedNum = needNum
                }
            );

            if (posts == null || posts.Count == 0)
            {
                return NotFound($"未找到板块ID为 {sectionId} 的帖子范围：{beginNum}-{beginNum + needNum - 1}");
            }

            return Ok(posts);
        }

        [HttpGet("user/{sectionId}")]
        public ActionResult<IEnumerable<UserBaseInfoResponse>> GetUsersBaseInfoInRangeBySectionId(
            [FromQuery] int sectionId,
            [FromQuery] int beginNum,
            [FromQuery] int needNum
        )
        {
            List<UserBModel>? users = postBll.GetUsers(
                new PostListBModel
                {
                    SectionId = sectionId,
                    BeginNum = beginNum,
                    NeedNum = needNum
                }
            );

            if (users == null || users.Count == 0)
            {
                logger.LogWarning(
                    "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表为空！",
                    sectionId,
                    beginNum,
                    needNum
                );
                return NotFound(
                    $"未找到板块ID为 {sectionId} 的用户信息范围：{beginNum}-{beginNum + needNum - 1}"
                );
            }

            logger.LogInformation(
                "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表",
                sectionId,
                beginNum,
                needNum
            );

            return Ok(ConvertToUserBaseInfoResponse(users));
        }

        [HttpPost("{sectionId}")]
        public ActionResult<int> Insert(int sectionId, [FromBody] PostInsertRequest request)
        {
            try
            {
                logger.LogInformation(
                    "用户({UserId})在板块({SectionId})发表了帖子",
                    request.UserId,
                    sectionId
                );
                int postId = postBll.AddPost(sectionId, request.ToPost());
                return Ok(postId);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "用户({UserId})在板块({SectionId})发表帖子时出错",
                    request.UserId,
                    sectionId
                );
                return StatusCode(500, "内部服务器错误");
            }
        }

        // 点赞与踩
        [HttpPut("{sectionId}/{postId}")]
        public ActionResult<string> Update(
            int sectionId,
            int postId,
            [FromBody] PostUpdateRequest request
        )
        {
            try
            {
                if (request.UpVote)
                {
                    postBll.UpVote(sectionId, postId);
                    // todo 需要知道点赞者id
                    logger.LogInformation(
                        "用户(暂无)在板块({sectionId})的帖子({postId})点赞了",
                        sectionId,
                        postId
                    );
                    return Ok("操作成功！");
                }
                else if (request.DownVote)
                {
                    postBll.DownVote(sectionId, postId);
                    // todo 需要知道踩者id
                    logger.LogInformation(
                        "用户(暂无)在板块({sectionId})的帖子({postId})点踩了",
                        sectionId,
                        postId
                    );
                    return Ok("操作成功！");
                }

                return BadRequest("无效的操作请求");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "更新帖子({postId})的状态时发生错误", postId);
                return StatusCode(500, "内部服务器错误");
            }
        }

        // 增加浏览量
        [HttpPut("view/{sectionId}/{postId}")]
        public IActionResult UpdateView(int sectionId, int postId)
        {
            try
            {
                logger.LogInformation("板块({sectionId})的帖子({postId})被浏览了", sectionId, postId);
                postBll.UpdateView(sectionId, postId);
                return Ok("浏览量更新成功");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "更新浏览量时发生错误", postId);
                return StatusCode(500, "内部服务器错误");
            }
        }

        private static List<UserBaseInfoResponse> ConvertToUserBaseInfoResponse(
            List<UserBModel> users
        )
        {
            return users.Select(u => u.ToUserBaseInfoResponse()).ToList();
        }
    }
}
