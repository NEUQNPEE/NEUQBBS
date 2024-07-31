using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebapiDemo.Models;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.BModels;

namespace WebapiDemo.Controllers
{
    [ApiController]
    // [Route("api/[controller]/[action]")]
    [Route("[controller]")]
    [EnableCors("any")]
    public class LoginApiController : ControllerBase
    {
        private readonly IUserBll userBll;

        public LoginApiController(IUserBll userBll)
        {
            this.userBll = userBll;
        }

        // 获取所有User
        [HttpGet("debug/all")]
        public List<UserBModel?>? DebugGetAll()
        {
            return userBll.DebugGetAllUsers();
        }

        // 按id获取User
        [HttpGet("debug/{id}")]
        public UserBModel? DebugGetById(int id)
        {
            return userBll.DebugGetUserById(id);
        }

        // 按id获取User,只取得可显示的字段
        [HttpGet("{id}")]
        public UserBModel? GetById(int id)
        {
            return userBll.GetUserById(id);
        }

        // 按id获取Username
        [HttpGet("username/{id}")]
        public string GetUserNameById(int id)
        {
            return userBll.GetUserNameById(id);
        }

        // 按id获取基本信息
        [HttpGet("baseinfo/{id}")]
        public UserBaseInfoResponse? GetBaseInfoById(int id)
        {
            return userBll.GetUserBaseInfoById(id)?.ToBaseResponse();
        }

        // 按id获取详细信息
        [HttpGet("info/{id}")]
        public UserInfoResponse? GetInfoById(int id)
        {
            return userBll.GetUserDetailInfoById(id)?.ToResponse();
        }

        // 检查登录
        [HttpPost("login")]
        public IActionResult CheckLogin([FromBody] LoginRequest request)
        {
            int userId;
            // 首先处理自动登录
            if (!string.IsNullOrEmpty(request.AutoLoginToken))
            {
                userId = userBll.CheckAutoLoginToken(request.AutoLoginToken);
                if (userId == -1)
                {
                    return BadRequest("Token无效");
                }

                Response.Headers.Add("Access-Control-Expose-Headers", "userId");
                Response.Headers.Add("userId", userId.ToString());

                return Ok("自动登录成功！");
            }

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("用户名或密码为空");
            }

            userId = userBll.CheckLogin(request.UserName, request.Password);
            if (userId == -1)
            {
                return BadRequest("用户名或密码错误");
            }

            if (!request.RememberMe)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "UserId");
                Response.Headers.Add("UserId", userId.ToString());
                return Ok("登录成功！");
            }
            else
            {
                string token = userBll.GenerateAutoLoginToken(userId);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Token生成失败");
                }

                Response.Headers.Add("Access-Control-Expose-Headers", "UserId,AutoLoginToken");
                Response.Headers.Add("UserId", userId.ToString());
                Response.Headers.Add("AutoLoginToken", token);

                return Ok("登录成功,Token生成成功");
            }
        }

        // 插入一条User数据
        [HttpPost]
        public IActionResult Insert([FromBody] UserInsertRequest request)
        {
            // 在浏览器中存储返回来的Id，如此在首次跳转登录页面时，可以直接填入用户名和密码（仅此一次）
            if (userBll.CheckUserExist(request.UserName))
            {
                return BadRequest("用户名已存在！");
            }

            int userId = userBll.AddUser(request.ToBModel());
            if (userId == -1)
            {
                return BadRequest("密码为空！");
            }
            else
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "UserId");
                Response.Headers.Add("UserId", userId.ToString());
                return Ok("注册成功！");
            }
        }

        // 更新数据
        [HttpPut]
        public string Update([FromBody] UserUpdateRequest request)
        {
            return userBll.UpdateUser(request.ToBModel());
        }

        // 删除数据
        [HttpDelete("{id}")]
        public string Remove(int id)
        {
            return userBll.RemoveUser(id);
        }
    }
}
