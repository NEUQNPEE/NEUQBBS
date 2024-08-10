using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.EPost;
using WebApiDemo.Entities.ESection;

namespace WebApiDemo.Controllers;

/// <summary>
/// 板块相关API
/// </summary>
[ApiController]
[Route("[controller]")]
[EnableCors("any")]
public class SectionApiController : ControllerBase
{
    private readonly ISectionBll sectionBll;
    private readonly IUserBll userBll;
    private readonly IPostBll postBll;
    private readonly ILogger<SectionApiController> logger;
/// <summary>
/// 构造函数
/// </summary>
/// <param name="sectionBll"></param>
/// <param name="userBll"></param>
/// <param name="postBll"></param>
/// <param name="logger"></param>
    public SectionApiController(
        ISectionBll sectionBll,
        IUserBll userBll,
        IPostBll postBll,
        ILogger<SectionApiController> logger
    )
    {
        this.sectionBll = sectionBll;
        this.userBll = userBll;
        this.postBll = postBll;
        this.logger = logger;
    }
/// <summary>
/// 获取所有板块
/// </summary>
/// <returns></returns>
    [HttpGet("all")]
    public ActionResult<List<Section>> GetAllSections()
    {
        logger.LogDebug("获取所有板块对象");
        // return sectionBll.GetAllSections();
        var sections = sectionBll.GetAllSections();
        if (sections == null || sections.Count == 0)
        {
            logger.LogWarning("没有找到任何板块");
            return NotFound("没有找到任何板块");
        }
        return Ok(sections);
    }

    /// <summary>
    /// 获取板块id为{id}的板块对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<Section> GetSectionById(int id)
    {
        logger.LogInformation("获取板块id为{id}的板块对象", id);
        var section = sectionBll.GetSectionById(id);
        if (section == null)
        {
            logger.LogWarning("没有找到id为{id}的板块", id);
            return NotFound("没有找到id为{id}的板块");
        }
        return Ok(section);
    }

    /// <summary>
    /// 根据板块id({sectionId})获取本版所有主贴
    /// </summary>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    [HttpGet("mainposts/{sectionId}")]
    public ActionResult<List<Post>> GetAllMainPostsBySectionId(int sectionId)
    {
        logger.LogInformation("根据板块id({sectionId})获取本版所有主贴", sectionId);
        // return sectionBll.GetMainPosts(sectionId);
        var posts = sectionBll.GetMainPosts(sectionId);
        if (posts == null || posts.Count == 0)
        {
            logger.LogWarning("没有找到板块id({sectionId})下的任何主贴", sectionId);
            return NotFound("没有找到板块id({sectionId})下的任何主贴");
        }
        return Ok(posts);
    }

    /// <summary>
    /// 获取所有主贴的最后回复信息
    /// </summary>
    /// <param name="sectionId"></param>
    /// <param name="mainPostIds"></param>
    /// <returns></returns>
    [HttpPost("lastreplyinfo/{sectionId}")]
    public ActionResult<List<LastReplyInfoResponse>> GetAllLastReplyInfoByMainPostIds(
        int sectionId,
        [FromBody] List<int> mainPostIds
    )
    {
        if (mainPostIds == null || mainPostIds.Count == 0)
        {
            logger.LogWarning("主贴id列表为空，无法获取最后回复信息");
            return BadRequest("主贴id列表不能为空");
        }

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

        if (lastReplyPosts.Count == 0)
        {
            logger.LogWarning("没有找到板块id({sectionId})下的任何主贴的最后回复", sectionId);
            return NotFound("没有找到板块id({sectionId})下的任何主贴的最后回复");
        }

        List<LastReplyInfoResponse> lastReplyInfoResponses = new();

        foreach (Post lastReplyPost in lastReplyPosts)
        {
            lastReplyInfoResponses.Add(
                new LastReplyInfoResponse
                {
                    MainPostId = lastReplyPost.MainPostId,
                    Username = userBll.GetUserNameById(lastReplyPost.UserId),
                    ReplyTime = lastReplyPost.PublishTime
                }
            );
        }

        logger.LogInformation("获取板块id({sectionId})下所有主贴的最后回复信息", sectionId);
        return Ok(lastReplyInfoResponses);
    }
}
