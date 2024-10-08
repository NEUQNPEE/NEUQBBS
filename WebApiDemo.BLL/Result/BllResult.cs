namespace WebApiDemo.BLL.Result;

/// <summary>
/// 统一封装的返回结果类
/// </summary>
/// <typeparam name="T">返回的数据类型</typeparam>
public class BllResult<T>
{
    /// <summary>
    /// 表示操作是否成功
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 返回的消息，通常用于描述失败原因或操作结果
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 返回的数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 全参数方法
    /// </summary>
    /// <param name="isSuccess">操作是否成功</param>
    /// <param name="message">操作的返回消息</param>
    /// <param name="data">返回的数据</param>
    private BllResult(bool isSuccess, string? message, T? data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    /// <summary>
    /// 是否成功-返回信息
    /// </summary>
    /// <param name="isSuccess">操作是否成功</param>
    /// <param name="message">操作的返回消息</param>
    private BllResult(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = default;
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    /// <param name="isSuccess">操作是否成功</param>
    private BllResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Message = default;
        Data = default;
    }

    /// <summary>
    /// 成功返回（无参）
    /// </summary>
    public static BllResult<T> Success()
    {
        return new BllResult<T>(true);
    }

    /// <summary>
    /// 成功返回（数据）
    /// </summary>
    /// <param name="data">返回的数据</param>
    public static BllResult<T> Success(T? data)
    {
        return new BllResult<T>(true, null, data);
    }

    /// <summary>
    /// 成功返回（返回信息 + 数据）
    /// </summary>
    /// <param name="message">返回信息</param>
    /// <param name="data">返回数据</param>
    public static BllResult<T> Success(string? message, T? data)
    {
        return new BllResult<T>(true, message, data);
    }

    /// <summary>
    /// 失败返回（无参）
    /// </summary>
    public static BllResult<T> Failure()
    {
        return new BllResult<T>(false);
    }

    /// <summary>
    /// 失败返回（数据）
    /// </summary>
    /// <param name="data">返回的数据</param>
    public static BllResult<T> Failure(T? data)
    {
        return new BllResult<T>(false, null, data);
    }

    /// <summary>
    /// 失败返回（返回信息）
    /// </summary>
    /// <param name="message">返回信息</param>
    public static BllResult<T> Failure(string? message)
    {
        return new BllResult<T>(false, message);
    }

    /// <summary>
    /// 失败返回（返回信息 + 数据）
    /// </summary>
    /// <param name="message">返回信息</param>
    /// <param name="data">返回数据</param>
    public static BllResult<T> Failure(string? message, T? data)
    {
        return new BllResult<T>(false, message, data);
    }
}
