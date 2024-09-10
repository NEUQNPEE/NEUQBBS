using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiDemo.Filter
{
    /// <summary>
    /// 全局异常处理（实际上是控制器级别的异常处理）
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    /// <param name="logger"></param>
    public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger = logger;

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(
                context.Exception,
                "发生未处理异常。请求路径：{Path},查询字符串：{QueryString},User:{User}",
                context.HttpContext.Request.Path,
                context.HttpContext.Request.QueryString,
                context.HttpContext.User.Identity?.Name
            );

            // 设置响应状态码为 500 并返回错误信息
            context.Result = new ObjectResult(new { Message = "未知错误，请重试" }) { StatusCode = 500 };
            context.ExceptionHandled = true;
        }
    }
}
