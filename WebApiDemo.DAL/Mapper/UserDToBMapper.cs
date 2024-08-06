using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EUser;

namespace WebApiDemo.DAL.Mapper
{
    public static class UserDToBMapper
    {
        public static UserBModel ToUserBModel(this User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return new UserBModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                NickName = user.NickName,
                Gender = user.Gender.ToString(),
                Signature = user.Signature,
                Avatar = user.Avatar,
                RegisterTime = user.RegisterTime,
                LastLoginTime = user.LastLoginTime,
                UserLevel = user.UserLevel,
                Points = user.Points,
                IsDeleted = user.IsDeleted
            };
        }

        // 可公开属性包括：Id, UserName, NickName,Gender, Signature, Avatar, RegisterTime, LastLoginTime, UserLevel, Points, IsDeleted
        public static UserBModel ToPublicUserBModel(this User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return new UserBModel
            {
                Id = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Gender = user.Gender.ToString(),
                Signature = user.Signature,
                Avatar = user.Avatar,
                RegisterTime = user.RegisterTime,
                LastLoginTime = user.LastLoginTime,
                UserLevel = user.UserLevel,
                Points = user.Points,
                IsDeleted = user.IsDeleted
            };
        }
    }
}