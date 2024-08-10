namespace WebApiDemo.Entities.ESection;

/// <summary>
/// 版块模型，包含版块的基本信息。
/// </summary>
public class Section
{
    /// <summary>
    /// 版块 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 版块所属区域
    /// </summary>
    public required string Area { get; set; }

    /// <summary>
    /// 版块图标
    /// </summary>
    public required string Icon { get; set; }

    /// <summary>
    /// 版块名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 版块描述
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// 版块对应的表名
    /// </summary>
    public required string TableName { get; set; }
}
