using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.DAL.Mapper
{
    public static class UserBToDMapper
    {
        // 对User提供一个扩展方法，对UserBModel中每一个非空属性赋值给User
        public static User UpdateFromBModel(this User user, UserBModel userBModel)
        {
            user.Id = userBModel.Id;

            if (userBModel.UserName != null)
            {
                user.UserName = userBModel.UserName;
            }

            if (userBModel.Password != null)
            {
                user.Password = userBModel.Password;
            }

            if (userBModel.NickName != null)
            {
                user.NickName = userBModel.NickName;
            }

            if (userBModel.Gender != null)
            {
                user.Gender = Enum.TryParse<Gender>(userBModel.Gender, out var parsedGender)
                    ? parsedGender
                    : (Gender?)null;
            }

            if (userBModel.Signature != null)
            {
                user.Signature = userBModel.Signature;
            }

            if (userBModel.Avatar != null)
            {
                user.Avatar = userBModel.Avatar;
            }

            user.LastLoginTime = userBModel.LastLoginTime;

            user.UserLevel = userBModel.UserLevel;

            user.Points = userBModel.Points;

            user.IsDeleted = userBModel.IsDeleted;

            return user;
        }

        public static User ToUser(this UserBModel userBModel)
        {
            return new User
            {
                Id = userBModel.Id,
                UserName = userBModel.UserName ?? string.Empty,
                Password = userBModel.Password ?? string.Empty,
                NickName = userBModel.NickName,
                Gender = Enum.TryParse<Gender>(userBModel.Gender, out var parsedGender)
                    ? parsedGender
                    : (Gender?)null,
                Signature = userBModel.Signature,
                Avatar = userBModel.Avatar,
                LastLoginTime = userBModel.LastLoginTime,
                UserLevel = userBModel.UserLevel,
                Points = userBModel.Points,
                IsDeleted = userBModel.IsDeleted
            };
        }
    }
}
