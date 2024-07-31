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

        public PostApiController(IPostBll postBll)
        {
            this.postBll = postBll;
        }

        [HttpGet("allpost/{sectionId}")]
        public List<Post?>? GetAllPost(int sectionId)
        {
            return postBll.GetAllPosts(sectionId);
        }

        [HttpGet("{sectionId}/{mainPostId}")]
        public List<Post?>? GetPosts(int sectionId, int mainPostId)
        {
            return postBll.GetPosts(sectionId, mainPostId);
        }

        [HttpGet("alluser/{sectionId}")]
        public List<UserBaseInfoResponse?>? GetAllUser(int sectionId)
        {
            List<UserBModel?>? user = postBll.GetAllUsers(sectionId);
            if (user == null)
            {
                return null;
            }

            List<UserBaseInfoResponse?>? result = new();
            foreach (UserBModel? u in user)
            {
                if (u == null)
                {
                    continue;
                }
                result.Add(
                    new UserBaseInfoResponse
                    {
                        Id = u.Id,
                        UserName = u.UserName ?? "用户名空引用！",
                        RegisterTime = u.RegisterTime.ToString("yyyy-MM-dd"),
                        Points = u.Points
                    }
                );
            }

            return result;
        }

        [HttpGet("post/{sectionId}")]
        public List<Post?>? GetPosts(int sectionId, [FromBody] PostGetInRangeRequest request)
        {
            return postBll.GetPosts(
                new PostListBModel
                {
                    SectionId = sectionId,
                    BeginNum = request.BeginNum,
                    NeedNum = request.NeedNum
                }
            );
        }

        [HttpGet("user/{sectionId}")]
        public List<UserBaseInfoResponse?>? GetUsers(
            int sectionId,
            [FromBody] PostGetInRangeRequest request
        )
        {
            List<UserBModel?>? user = postBll.GetUsers(
                new PostListBModel
                {
                    SectionId = sectionId,
                    BeginNum = request.BeginNum,
                    NeedNum = request.NeedNum
                }
            );

            if (user == null)
            {
                return null;
            }

            List<UserBaseInfoResponse?>? result = new();
            foreach (UserBModel? u in user)
            {
                if (u == null)
                {
                    continue;
                }
                result.Add(
                    new UserBaseInfoResponse
                    {
                        Id = u.Id,
                        UserName = u.UserName ?? "用户名空引用！",
                        RegisterTime = u.RegisterTime.ToString("yyyy-MM-dd"),
                        Points = u.Points
                    }
                );
            }

            return result;
        }

        [HttpPost("{sectionId}")]
        public int Insert(int sectionId, [FromBody] PostInsertRequest request)
        {
            return postBll.AddPost(sectionId, request.ToPost());
        }

        // 点赞与踩
        [HttpPut("{sectionId}/{postId}")]
        public string Update(int sectionId, int postId, [FromBody] PostUpdateRequest request)
        {
            if (request.UpVote)
            {
                postBll.UpVote(sectionId, postId);
                return "操作成功！";
            }
            else if (request.DownVote)
            {
                postBll.DownVote(sectionId, postId);
                return "操作成功！";
            }

            return "操作失败！";
        }

        // 增加浏览量
        [HttpPut("view/{sectionId}/{postId}")]
        public void UpdateView(int sectionId, int postId)
        {
            postBll.UpdateView(sectionId, postId);
        }
    }
}
