using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models;

/// <summary>
/// 获取指定范围内的帖子请求
/// </summary>
public class PostGetInRangeRequest
{
    /// <summary>
    /// 起始数量
    /// </summary>
    public int BeginNum { get; set; }
    /// <summary>
    /// 需要数量
    /// </summary>
    public int NeedNum { get; set; }
}

/// <summary>
/// 转换请求到业务模型的扩展方法
/// </summary>
public static class PostGetInRangeRequestVToBPostList
{
    /// <summary>
    /// 将 <see cref="PostGetInRangeRequest"/> 转换为 <see cref="PostListBModel"/>
    /// </summary>
    /// <param name="request"></param>
    /// <returns>转换后的帖子列表模型</returns>
    public static PostListBModel ToPostListBModel(this PostGetInRangeRequest request)
    {
        return new PostListBModel
        {
            SectionId = 0,
            BeginNum = request.BeginNum,
            NeedNum = request.NeedNum
        };
    }
}
