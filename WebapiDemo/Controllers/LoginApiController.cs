/*
 * @Author       : NieFire planet_class@foxmail.com
 * @Date         : 2024-05-16 15:10:29
 * @LastEditors  : NieFire planet_class@foxmail.com
 * @LastEditTime : 2024-09-07 20:37:15
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
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.Controllers;

/// <summary>
/// 用户登录相关API
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="userBll"></param>
/// <param name="logger"></param>
[ApiController]
// [Route("api/[controller]/[action]")]
[Route("[controller]")]
// todo 处理跨域
[EnableCors("any")]
public class LoginApiController(IUserBll userBll, ILogger<LoginApiController> logger) : ControllerBase
{

    /// <summary>
    /// debug 获取所有User
    /// </summary>
    /// <returns></returns>
    [HttpGet("debug/all")]
    public ActionResult<List<User>> DebugGetAll()
    {
        logger.LogDebug("获取所有User");
        var result = userBll.DebugGetAllUsers();
        if (!result.IsSuccess)
        {
            return NotFound("没有找到任何用户");
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// debug 按id获取User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("debug/{id}")]
    public ActionResult<User> DebugGetById(int id)
    {
        logger.LogDebug("按id获取User全部信息");
        var result = userBll.DebugGetUserById(id);
        if (!result.IsSuccess)
        {
            return NotFound("用户不存在");
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// 根据id获取用户信息
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    [HttpGet("userinfos")]
    public ActionResult<List<UserInfoResponse>> GetUserInfoById([FromQuery] IEnumerable<int> ids, [FromQuery] string fields)
    {
        var result = userBll.GetUserInfoById(ids, fields);
        if (!result.IsSuccess)
        {
            return NotFound("用户不存在");
        }
        return Ok(result.Data!.ToResponse());
    }

    /// <summary>
    /// 检查登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public IActionResult CheckLogin([FromBody] LoginRequest request)
    {
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

        var result = userBll.CheckLogin(request.UserName, request.Password);
        if (!result.IsSuccess)
        {
            logger.LogWarning("用户:{userName}的登录失败", request.UserName);
            return BadRequest("用户名或密码错误");
        }

        if (!request.RememberMe)
        {
            AddHeader("UserId", result.Data.ToString());
            logger.LogInformation("用户:{userName}的登录成功，不记住密码", request.UserName);
            return Ok("登录成功！");
        }
        else
        {
            string token = GenerateAutoLoginToken(result.Data);
            if (token == string.Empty)
            {
                return BadRequest("自动登录Token生成失败");
            }

            // UserId的键值对
            Dictionary<string, string> tokens =
                new() { { "UserId", result.Data.ToString() }, { "AutoLoginToken", token } };

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
        var result = userBll.CheckUserExist(request.UserName);
        if (result.IsSuccess)
        {
            logger.LogWarning("用户:{userName}已存在", request.UserName);
            return BadRequest("用户名已存在!");
        }

        var result2 = userBll.AddUser(request.ToUser());
        if (!result2.IsSuccess)
        {
            logger.LogWarning("前端传入用户名或密码为空的注册请求!");
            return BadRequest("用户名或密码为空!");
        }
        else
        {
            AddHeader("UserId", result2.Data.ToString());
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
        var result = userBll.UpdateUser(request.ToBModel());
        if (!result.IsSuccess)
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
        var result = userBll.RemoveUser(id);
        if (!result.IsSuccess)
        {
            return BadRequest("删除失败");
        }
        return Ok("删除成功");
    }
    /// <summary>
    /// 自动登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private bool AutoLogin(LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.AutoLoginToken))
        {
            return false;
        }

        var result = userBll.CheckAutoLoginToken(request.AutoLoginToken);
        if (!result.IsSuccess)
        {
            logger.LogWarning("用户:{userName}的自动登录Token无效", request.UserName);
            return false;
        }

        AddHeader("UserId", result.Data.ToString());

        logger.LogInformation("用户:{userName}的自动登录成功", request.UserName);
        return true;
    }
    /// <summary>
    /// 自动登录Token生成
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string GenerateAutoLoginToken(int userId)
    {
        var result = userBll.GenerateAutoLoginToken(userId);
        if (!result.IsSuccess)
        {
            logger.LogWarning("id为{id}的用户Token生成失败", userId);
            return string.Empty;
        }
        return result.Data!;
    }
    /// <summary>
    /// 添加响应头
    /// </summary>
    /// <param name="tokens"></param>
    private void AddHeader(Dictionary<string, string> tokens)
    {
        Response.Headers.AccessControlExposeHeaders = string.Join(",", tokens.Keys);

        foreach (KeyValuePair<string, string> token in tokens)
        {
            Response.Headers[token.Key] = token.Value;
        }
    }
    /// <summary>
    /// 添加响应头
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void AddHeader(string key, string value)
    {
        Response.Headers.AccessControlExposeHeaders = key;
        Response.Headers[key] = value;
    }
}
