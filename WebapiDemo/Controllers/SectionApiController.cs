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
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sectionBll"></param>
/// <param name="userBll"></param>
/// <param name="postBll"></param>
/// <param name="logger"></param>
[ApiController]
[Route("[controller]")]
[EnableCors("any")]
public class SectionApiController(
    ISectionBll sectionBll,
    IUserBll userBll,
    IPostBll postBll,
    ILogger<SectionApiController> logger
    ) : ControllerBase
{

    /// <summary>
    /// 获取所有板块
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public ActionResult<List<Section>> GetAllSections()
    {
        logger.LogDebug("获取所有板块对象");

        var result = sectionBll.GetAllSections();
        if (!result.IsSuccess)
        {
            logger.LogWarning("没有找到任何板块");
            return NotFound("没有找到任何板块");
        }
        return Ok(result.Data);
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

        var result = sectionBll.GetSectionById(id);
        if (!result.IsSuccess)
        {
            logger.LogWarning("没有找到id为{id}的板块", id);
            return NotFound("没有找到id为{id}的板块");
        }
        return Ok(result.Data);
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

        var result = sectionBll.GetMainPosts(sectionId);
        if (!result.IsSuccess)
        {
            logger.LogWarning("没有找到板块id({sectionId})下的任何主贴", sectionId);
            return NotFound("没有找到板块id({sectionId})下的任何主贴");
        }
        return Ok(result.Data);
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

        var lastReplyPosts = new List<Post>();
        foreach (int mainPostId in mainPostIds)
        {
            var result = postBll.GetLastReplyPosts(sectionId, mainPostId);
            if (result.IsSuccess)
            {
                lastReplyPosts.Add(result.Data!);
            }
        }

        if (lastReplyPosts.Count == 0)
        {
            logger.LogWarning("没有找到板块id({sectionId})下的任何主贴的最后回复", sectionId);
            return NotFound("没有找到板块id({sectionId})下的任何主贴的最后回复");
        }

        var lastReplyInfoResponses = new List<LastReplyInfoResponse>();

        foreach (Post lastReplyPost in lastReplyPosts)
        {
            var userInfoResult = userBll.GetUserInfoById(lastReplyPost.UserId, "username");
            lastReplyInfoResponses.Add(
                new LastReplyInfoResponse
                {
                    MainPostId = lastReplyPost.MainPostId,
                    Username = userInfoResult.IsSuccess ? userInfoResult.Data!.UserName! : "未知用户",
                    ReplyTime = lastReplyPost.PublishTime
                }
            );
        }

        logger.LogInformation("获取板块id({sectionId})下所有主贴的最后回复信息", sectionId);
        return Ok(lastReplyInfoResponses);
    }
}
