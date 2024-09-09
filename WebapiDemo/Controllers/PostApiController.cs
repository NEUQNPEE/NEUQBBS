using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EPost;

namespace WebApiDemo.Controllers;

/// <summary>
/// 帖子相关API
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="postBll"></param>
/// <param name="logger"></param>
[ApiController]
[Route("[controller]")]
[EnableCors("any")]
public class PostApiController(IPostBll postBll, ILogger<PostApiController> logger) : ControllerBase
{

    /// <summary>
    /// 获取板块({sectionId})的所有帖子
    /// </summary>
    /// <param name="sectionId">板块id</param>
    /// <returns></returns>
    [HttpGet("allpost/{sectionId}")]
    public ActionResult<List<Post>> GetAllPostsBySectionId(int sectionId)
    {
        logger.LogDebug("获取板块({sectionId})的所有帖子", sectionId);
        // var posts = postBll.GetAllPosts(sectionId).Data;
        // if (posts == null || posts.Count == 0)
        // {
        //     return NotFound($"未找到板块ID为 {sectionId} 的帖子");
        // }
        // return Ok(posts);

        var result = postBll.GetAllPosts(sectionId);
        if (!result.IsSuccess)
        {
            return NotFound($"未找到板块ID为 {sectionId} 的帖子");
        }
        return Ok(result.Data);
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
        // var posts = postBll.GetPosts(sectionId, mainPostId).Data;
        // if (posts == null || posts.Count == 0)
        // {
        //     return NotFound($"未找到板块ID为 {sectionId} 的主贴ID为 {mainPostId} 的回复");
        // }
        // return Ok(posts);

        var result = postBll.GetPosts(sectionId, mainPostId);
        if (!result.IsSuccess)
        {
            return NotFound($"未找到板块ID为 {sectionId} 的主贴ID为 {mainPostId} 的回复");
        }
        return Ok(result.Data);
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
        // List<UserBModel>? users = postBll.GetAllUsers(sectionId).Data;
        // if (users == null || users.Count == 0)
        // {
        //     return NotFound("未找到用户基本信息");
        // }

        // logger.LogInformation("获取在板块({sectionId})中发过帖的所有用户的基本信息", sectionId);
        // return Ok(users.Select(u => u.ToUserBaseInfoResponse()));

        var result = postBll.GetAllUsers(sectionId);
        if (!result.IsSuccess||result.Data == null)
        {
            return NotFound("未找到用户基本信息");
        }
        logger.LogInformation("获取在板块{sectionId}中发过帖的所有用户的基本信息", sectionId);
        return Ok(result.Data.Select(u => u.ToUserBaseInfoResponse()));
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
        int sectionId,
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
        // var posts = postBll.GetPosts(
        //     new PostListBModel
        //     {
        //         SectionId = sectionId,
        //         BeginNum = beginNum,
        //         NeedNum = needNum
        //     }
        // ).Data;

        // if (posts == null || posts.Count == 0)
        // {
        //     return NotFound($"未找到板块ID为 {sectionId} 的帖子范围：{beginNum}-{beginNum + needNum - 1}");
        // }

        // return Ok(posts);

        var result = postBll.GetPosts(
            new PostListBModel
            {
                SectionId = sectionId,
                BeginNum = beginNum,
                NeedNum = needNum
            }
        );
        if (!result.IsSuccess || result.Data == null)
        {
            return NotFound($"未找到板块ID为 {sectionId} 的帖子范围：{beginNum}-{beginNum + needNum - 1}");
        }
        return Ok(result.Data);
    }

/// <summary>
/// 获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息
/// </summary>
/// <param name="sectionId"></param>
/// <param name="beginNum"></param>
/// <param name="needNum"></param>
/// <returns></returns>
    [HttpGet("user/{sectionId}")]
    public ActionResult<IEnumerable<UserBaseInfoResponse>> GetUsersBaseInfoInRangeBySectionId(
        int sectionId,
        [FromQuery] int beginNum,
        [FromQuery] int needNum
    )
    {
        // List<UserBModel>? users = postBll.GetUsers(
        //     new PostListBModel
        //     {
        //         SectionId = sectionId,
        //         BeginNum = beginNum,
        //         NeedNum = needNum
        //     }
        // ).Data;

        // if (users == null || users.Count == 0)
        // {
        //     logger.LogWarning(
        //         "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表为空！",
        //         sectionId,
        //         beginNum,
        //         needNum
        //     );
        //     return NotFound($"未找到板块ID为 {sectionId} 的用户信息范围：{beginNum}-{beginNum + needNum - 1}");
        // }

        // logger.LogInformation(
        //     "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表",
        //     sectionId,
        //     beginNum,
        //     needNum
        // );

        // return Ok(ConvertToUserBaseInfoResponse(users));

        var result = postBll.GetUsers(
            new PostListBModel
            {
                SectionId = sectionId,
                BeginNum = beginNum,
                NeedNum = needNum
            }
        );
        if (!result.IsSuccess)
        {
            logger.LogWarning(
                "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表为空！",
                sectionId,
                beginNum,
                needNum
            );
            return NotFound($"未找到板块ID为 {sectionId} 的用户信息范围：{beginNum}-{beginNum + needNum - 1}");
        }
        logger.LogInformation(
            "获取板块({sectionId})自第{beginNum}条开始的{needNum}条帖子的用户基本信息表",
            sectionId,
            beginNum,
            needNum
        );

        return Ok(result.Data!.Select(u => u.ToUserBaseInfoResponse()).ToList());
    }
    /// <summary>
    /// 发表帖子
    /// </summary>
    /// <param name="sectionId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{sectionId}")]
    public ActionResult<int> Insert(int sectionId, [FromBody] PostInsertRequest request)
    {
        logger.LogInformation("用户({UserId})在板块({SectionId})发表了帖子", request.UserId, sectionId);
            
        var result = postBll.AddPost(sectionId, request.ToPost());
        if (!result.IsSuccess)
        {
            logger.LogError("用户({UserId})在板块({SectionId})发表帖子时出错", request.UserId, sectionId);
            return StatusCode(500, "内部服务器错误");
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// 更新帖子相关信息，目前只支持点赞与点踩
    /// </summary>
    /// <param name="sectionId"></param>
    /// <param name="postId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{sectionId}/{postId}")]
    public ActionResult<string> Update(
        int sectionId,
        int postId,
        [FromBody] PostUpdateRequest request
    )
    {
        if (request.UpVote)
        {
            var result = postBll.UpVote(sectionId, postId);
            if (!result.IsSuccess)
            {
                logger.LogError("用户(暂无)在板块({SectionId})的帖子({PostId})点赞时出错", sectionId, postId);
                return StatusCode(500, "内部服务器错误");
            }
            logger.LogInformation("用户(暂无)在板块({SectionId})的帖子({PostId})点赞了", sectionId, postId);
            return Ok("操作成功");
        }
        else if (request.DownVote)
        {
            var result = postBll.DownVote(sectionId, postId);
            if (!result.IsSuccess)
            {
                logger.LogError("用户(暂无)在板块({SectionId})的帖子({PostId})点踩时出错", sectionId, postId);
                return StatusCode(500, "内部服务器错误");
            }
            logger.LogInformation("用户(暂无)在板块({SectionId})的帖子({PostId})点踩了", sectionId, postId);
            return Ok("操作成功");
        }
        return BadRequest("无效的操作请求");
    }

    /// <summary>
    /// 更新浏览量
    /// </summary>
    /// <param name="sectionId"></param>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpPut("view/{sectionId}/{postId}")]
    public IActionResult UpdateView(int sectionId, int postId)
    {
        // try
        // {
        //     logger.LogInformation("板块({sectionId})的帖子({postId})被浏览了", sectionId, postId);
        //     postBll.UpdateView(sectionId, postId);
        //     return Ok("浏览量更新成功");
        // }
        // catch (Exception ex)
        // {
        //     logger.LogError(ex, "更新浏览量时发生错误。板块ID: {sectionId}, 帖子ID: {postId}", sectionId, postId);
        //     return StatusCode(500, "内部服务器错误");
        // }

        var result = postBll.UpdateView(sectionId, postId);
        if (!result.IsSuccess)
        {
            logger.LogError("更新浏览量时发生错误。板块ID: {sectionId}, 帖子ID: {postId}", sectionId, postId);
            return StatusCode(500, "内部服务器错误");
        }
        logger.LogInformation("板块({sectionId})的帖子({postId})被浏览了", sectionId, postId);
        return Ok("浏览量更新成功");
    }
}
