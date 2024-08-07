using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models
{
    public class UserInfoResponse
    {
        public int Id { get; set; }
        public required string UserName { get; set; }

        public required string NickName { get; set; }

        public required string Gender { get; set; }

        public required string Signature { get; set; }

        public required string Avatar { get; set; }

        public required string RegisterTime { get; set; }

        public required string LastLoginTime { get; set; }

        public int UserLevel { get; set; }

        public int Points { get; set; }
    }

    public static class UserBToVUserInfoResponseMapper { 
        public static UserInfoResponse ToResponse(this UserBModel userInfoBModel)
        {
            return new UserInfoResponse
            {
                Id = userInfoBModel.Id,
                UserName = userInfoBModel.UserName?? "用户名空引用！",
                NickName = userInfoBModel.NickName?? "昵称空引用！",
                Gender = userInfoBModel.Gender?? "性别空引用！",
                Signature = userInfoBModel.Signature?? "签名空引用！",
                Avatar = userInfoBModel.Avatar?? "头像空引用！",
                RegisterTime = userInfoBModel.RegisterTime.ToString("yyyy-MM-dd"),
                LastLoginTime = userInfoBModel.LastLoginTime.ToString("yyyy-MM-dd"),
                UserLevel = userInfoBModel.UserLevel,
                Points = userInfoBModel.Points
            };
        }
    }
}
