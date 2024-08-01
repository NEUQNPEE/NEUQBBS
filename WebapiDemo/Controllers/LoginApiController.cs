/*
 * @Author       : NieFire planet_class@foxmail.com
 * @Date         : 2024-05-16 15:10:29
 * @LastEditors  : NieFire planet_class@foxmail.com
 * @LastEditTime : 2024-08-01 17:24:19
 * @FilePath     : \CS_Computer-Science-and-Technologye:\CX\WebapiDemo\WebapiDemo\Controllers\LoginApiController.cs
 * @Description  : 用户登录相关API
 * ( ﾟ∀。)只要加满注释一切都会好起来的( ﾟ∀。)
 * Copyright (c) 2024 by NieFire, All Rights Reserved. 

 update 2024年8月1日 本文件补全日志功能
 */
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebapiDemo.Models;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.BModels;

/**
* readme
* 1. 在 Web API 中，建议尽量使用 IActionResult 或 ActionResult<T> 作为返回类型，以便更好地控制返回的 HTTP 状态码和内容。例如，DebugGetAll 方法可以改为 ActionResult<List<UserBModel?>>，以便在没有用户时返回 NotFound 状态。
* 3. 你启用了 EnableCors("any")。根据安全要求，建议在生产环境中限制允许的域，而不是允许所有域。
*/

namespace WebapiDemo.Controllers
{
    [ApiController]
    // [Route("api/[controller]/[action]")]
    [Route("[controller]")]
    [EnableCors("any")]
    public class LoginApiController : ControllerBase
    {
        private readonly IUserBll userBll;
        private readonly ILogger<LoginApiController> logger;

        public LoginApiController(IUserBll userBll, ILogger<LoginApiController> logger)
        {
            this.userBll = userBll;
            this.logger = logger;
        }

        // 获取所有User
        [HttpGet("debug/all")]
        public List<UserBModel?>? DebugGetAll()
        {
            logger.LogDebug("获取所有User");
            return userBll.DebugGetAllUsers();
        }

        // 按id获取User
        [HttpGet("debug/{id}")]
        public UserBModel? DebugGetById(int id)
        {
            logger.LogDebug("按id获取User全部信息");
            return userBll.DebugGetUserById(id);
        }

        // 按id获取User,只取得可显示的字段
        [HttpGet("{id}")]
        public UserBModel? GetById(int id)
        {
            logger.LogInformation("id为{id}的用户获取了可见信息", id);
            return userBll.GetUserById(id);
        }

        // 按id获取Username
        [HttpGet("username/{id}")]
        public string GetUserNameById(int id)
        {
            logger.LogInformation("id为{id}的用户获取了用户名", id);
            return userBll.GetUserNameById(id);
        }

        // 按id获取基本信息
        [HttpGet("baseinfo/{id}")]
        public UserBaseInfoResponse? GetBaseInfoById(int id)
        {
            logger.LogInformation("id为{id}的用户获取了基本信息", id);
            return userBll.GetUserBaseInfoById(id)?.ToBaseResponse();
        }

        // 按id获取详细信息
        [HttpGet("info/{id}")]
        public UserInfoResponse? GetInfoById(int id)
        {
            logger.LogInformation("id为{id}的用户获取了详细信息", id);
            return userBll.GetUserDetailInfoById(id)?.ToResponse();
        }

        // 检查登录
        [HttpPost("login")]
        public IActionResult CheckLogin([FromBody] LoginRequest request)
        {
            int userId;
            // 首先处理自动登录
            if (AutoLogin(request))
            {
                return Ok("自动登录成功");
            }

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                logger.LogWarning("前端传入用户名或密码为空的登陆请求");
                return BadRequest("用户名或密码为空");
            }

            userId = userBll.CheckLogin(request.UserName, request.Password);
            if (userId == -1)
            {
                logger.LogWarning("用户:{userName}的登录失败", request.UserName);
                return BadRequest("用户名或密码错误");
            }

            if (!request.RememberMe)
            {
                AddUserIdHeader(userId);
                logger.LogInformation("用户:{userName}的登录成功，不记住密码", request.UserName);
                return Ok("登录成功！");
            }
            else
            {
                string token = GenerateAutoLoginToken(userId);
                if (token == string.Empty)
                {
                    return BadRequest("自动登录Token生成失败");
                }

                AddUserIdHeader(userId);
                Response.Headers.Add("AutoLoginToken", token);

                logger.LogInformation("用户:{userName}的登录成功，记住密码", request.UserName);
                return Ok("登录成功,Token生成成功");
            }
        }

        private bool AutoLogin(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.AutoLoginToken))
            {
                return false;
            }

            int userId = userBll.CheckAutoLoginToken(request.AutoLoginToken);
            if (userId == -1)
            {
                // todo 根据用户名获取其id
                logger.LogWarning("用户:{userName}的自动登录Token无效", request.UserName);
                return false;
            }

            AddUserIdHeader(userId);

            logger.LogInformation("用户:{userName}的自动登录成功", request.UserName);
            return true;
        }

        private string GenerateAutoLoginToken(int userId)
        {
            string token = userBll.GenerateAutoLoginToken(userId);

            if (string.IsNullOrEmpty(token))
            {
                logger.LogWarning("id为{id}的用户Token生成失败", userId);
                return string.Empty;
            }

            return token;
        }

        // 插入一条User数据
        [HttpPost]
        public IActionResult Insert([FromBody] UserInsertRequest request)
        {
            // 在浏览器中存储返回来的Id，如此在首次跳转登录页面时，可以直接填入用户名和密码（仅此一次）
            if (userBll.CheckUserExist(request.UserName))
            {
                logger.LogWarning("用户:{userName}已存在", request.UserName);
                return BadRequest("用户名已存在！");
            }

            int userId = userBll.AddUser(request.ToBModel());
            if (userId == -1)
            {
                logger.LogWarning("前端传入用户名或密码为空的注册请求！");
                return BadRequest("用户名或密码为空！");
            }
            else
            {
                AddUserIdHeader(userId);
                logger.LogInformation("用户:{userName}的注册成功", request.UserName);
                return Ok("注册成功！");
            }
        }

        // 更新数据
        [HttpPut]
        public string Update([FromBody] UserUpdateRequest request)
        {
            logger.LogInformation("用户更新数据请求: {request}", request);
            return userBll.UpdateUser(request.ToBModel());
        }

        // 删除数据
        [HttpDelete("{id}")]
        public string Remove(int id)
        {
            logger.LogInformation("用户删除数据请求: {id}", id);
            return userBll.RemoveUser(id);
        }

        private void AddUserIdHeader(int userId)
        {
            Response.Headers.Add("Access-Control-Expose-Headers", "UserId");
            Response.Headers.Add("UserId", userId.ToString());
        }
    }
}
