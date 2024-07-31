using WebApiDemo.Entities.BModels;

namespace WebapiDemo.Models
{
    public class UserBaseInfoResponse
    {
        public int Id { get; set; }
        public required string UserName { get; set; }

        // 此处受前端要求，只传递年月日
        public required string RegisterTime { get; set; }
        public int Points { get; set; }
    }

    public static class UserBToVUserBaseInfoResponseMapper
    {
        public static UserBaseInfoResponse ToBaseResponse(this UserBModel userBaseInfoBModel)
        {
            return new UserBaseInfoResponse
            {
                Id = userBaseInfoBModel.Id,
                UserName = userBaseInfoBModel.UserName?? "用户名空引用！",
                RegisterTime = userBaseInfoBModel.RegisterTime.ToString("yyyy-MM-dd"),
                Points = userBaseInfoBModel.Points
            };
        }
    }
}
