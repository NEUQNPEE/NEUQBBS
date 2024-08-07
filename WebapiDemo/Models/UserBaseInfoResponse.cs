using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models
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
        public static UserBaseInfoResponse ToUserBaseInfoResponse(this UserBModel u)
        {
            return new UserBaseInfoResponse
            {
                Id = u.Id,
                UserName = u.UserName?? "用户名空引用！",
                RegisterTime = u.RegisterTime.ToString("yyyy-MM-dd"),
                Points = u.Points
            };
        }
    }
}
