namespace WebApiDemo.Entities.BModels;

    /// <summary>
    /// 帖子列表的业务模型
    /// </summary>
    public class PostListBModel
    {
        /// <summary>
        /// 版块 ID
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// 起始数量
        /// </summary>
        public int BeginNum { get; set; }

        /// <summary>
        /// 需要数量
        /// </summary>
        public int NeedNum { get; set; }
    }
