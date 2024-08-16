/*
 * @Author       : NieFire planet_class@foxmail.com
 * @Date         : 2024-05-16 15:10:29
 * @LastEditors  : NieFire planet_class@foxmail.com
 * @LastEditTime : 2024-08-17 00:37:04
 * @FilePath     : \CS_Computer-Science-and-Technologye:\CX\WebapiDemo\WebApiDemo\Controllers\LoginApiController.cs
 * @Description  : 用户登录相关API
 * ( ﾟ∀。)只要加满注释一切都会好起来的( ﾟ∀。)
 * Copyright (c) 2024 by NieFire, All Rights Reserved.

 update 2024年8月1日 本文件补全日志功能
 */
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Controllers;

/// <summary>
/// 用户登录相关API
/// </summary>
[ApiController]
// [Route("api/[controller]/[action]")]
[Route("[controller]")]
// todo 处理跨域
[EnableCors("any")]
public class LoginApiController : ControllerBase
{
    private readonly IUserBll userBll;
    private readonly ILogger<LoginApiController> logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userBll"></param>
    /// <param name="logger"></param>
    public LoginApiController(IUserBll userBll, ILogger<LoginApiController> logger)
    {
        this.userBll = userBll;
        this.logger = logger;
    }

    /// <summary>
    /// debug 获取所有User
    /// </summary>
    /// <returns></returns>
    [HttpGet("debug/all")]
    public ActionResult<List<UserBModel>> DebugGetAll()
    {
        logger.LogDebug("获取所有User");
        return Ok(userBll.DebugGetAllUsers());
    }

    /// <summary>
    /// debug 按id获取User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("debug/{id}")]
    public ActionResult<UserBModel> DebugGetById(int id)
    {
        logger.LogDebug("按id获取User全部信息");
        var user = userBll.DebugGetUserById(id);
        if (user == null)
        {
            return NotFound("用户不存在");
        }
        return Ok(user);
    }

    /// <summary>
    /// 按id获取User,只取得可显示的字段
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<UserBModel> GetById(int id)
    {
        logger.LogInformation("id为{id}的用户获取了可见信息", id);
        var user = userBll.GetUserById(id);
        if (user == null)
        {
            return NotFound("用户不存在");
        }
        return Ok(user);
    }

    /// <summary>
    /// 按id获取Username
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("username/{id}")]
    public ActionResult<string> GetUserNameById(int id)
    {
        logger.LogInformation("id为{id}的用户获取了用户名", id);
        var username = userBll.GetUserNameById(id);
        if (string.IsNullOrEmpty(username))
        {
            return NotFound("用户不存在");
        }
        return Ok(username);
    }


    /// <summary>
    /// 按id获取基本信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("baseinfo/{id}")]
    public ActionResult<UserBaseInfoResponse> GetBaseInfoById(int id)
    {
        logger.LogInformation("id为{id}的用户获取了基本信息", id);
        var user = userBll.GetUserById(id)?.ToUserBaseInfoResponse();
        if (user == null)
        {
            return NotFound("用户不存在");
        }
        return Ok(user);
    }


    /// <summary>
    /// 按id获取详细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("info/{id}")]
    public ActionResult<UserInfoResponse> GetInfoById(int id)
    {
        logger.LogInformation("id为{id}的用户获取了详细信息", id);
        var userInfo = userBll.GetUserById(id)?.ToResponse();
        if (userInfo == null)
        {
            return NotFound("用户不存在");
        }
        return Ok(userInfo);
    }


    /// <summary>
    /// 检查登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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
            AddHeader("UserId", userId.ToString());
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

            // UserId的键值对
            Dictionary<string, string> tokens =
                new() { { "UserId", userId.ToString() }, { "AutoLoginToken", token } };

            AddHeader(tokens);

            logger.LogInformation("用户:{userName}的登录成功，记住密码", request.UserName);
            return Ok("登录成功,Token生成成功");
        }
    }

    /// <summary>
    /// 注册/插入一条User数据
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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
            AddHeader("UserId", userId.ToString());
            logger.LogInformation("用户:{userName}的注册成功", request.UserName);
            return Ok("注册成功！");
        }
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult Update([FromBody] UserUpdateRequest request)
    {
        logger.LogInformation("用户更新数据请求: {request}", request);
        string result = userBll.UpdateUser(request.ToBModel());
        if (result == "操作失败")
        {
            return BadRequest("更新失败");
        }
        return Ok("更新成功");
    }


    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        logger.LogInformation("用户删除数据请求: {id}", id);
        string result = userBll.RemoveUser(id);
        if (result == "操作失败")
        {
            return BadRequest("删除失败");
        }
        return Ok("删除成功");
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

        AddHeader("UserId", userId.ToString());

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

    private void AddHeader(Dictionary<string, string> tokens)
    {
        Response.Headers["Access-Control-Expose-Headers"] = string.Join(",", tokens.Keys);

        foreach (KeyValuePair<string, string> token in tokens)
        {
            Response.Headers[token.Key] = token.Value;
        }
    }

    private void AddHeader(string key, string value)
    {
        Response.Headers["Access-Control-Expose-Headers"] = key;
        Response.Headers[key] = value;
    }
}
