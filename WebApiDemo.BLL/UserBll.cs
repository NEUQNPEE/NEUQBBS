using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.Entities.BModels;
using WebApiDemo.Entities.EUser;
using WebApiDemoCommon;

namespace WebApiDemo.BLL
{
    public class UserBll : IUserBll
    {
        // readonly UserDal userDal = new();

        public List<UserBModel?>? DebugGetAllUsers()
        {
            return UserDal.DebugGetAllUsers();
        }

        public UserBModel? DebugGetUserById(int id)
        {
            return UserDal.DebugGetUserById(id);
        }

        public UserBModel? GetUserById(int id)
        {
            return UserDal.GetUserById(id);
        }

        public string GetUserNameById(int id)
        {
            UserBModel? user = UserDal.GetUserById(id);
            if (user == null)
            {
                return "未找到";
            }

            return user.UserName ?? "用户名空引用！";
        }

        public List<string> GetUserNamesByIds(List<int> ids)
        {
            List<string> userNames = new();
            foreach (int id in ids)
            {
                userNames.Add(GetUserNameById(id));
            }

            return userNames;
        }

        public UserBModel? GetUserByIdForPost(int id)
        {
            // 只保留userName,RigisterTime和Points字段,注意排除IsDeleted字段
            UserBModel? user = UserDal.GetUserById(id);
            if (user == null)
            {
                return null;
            }

            return new UserBModel
            {
                UserName = user.UserName,
                RegisterTime = user.RegisterTime,
                Points = user.Points
            };
        }

        public UserBModel? GetUserBaseInfoById(int id)
        {
            UserBModel? user = UserDal.GetUserById(id);
            if (user == null)
            {
                return null;
            }

            return new UserBModel
            {
                Id = user.Id,
                UserName = user.UserName,
                RegisterTime = user.RegisterTime,
                Points = user.Points
            };
        }

        public UserBModel? GetUserDetailInfoById(int id)
        {
            return UserDal.GetUserById(id);
        }

        public bool CheckUserExist(string userName)
        {
            return UserDal.CheckUserExist(userName);
        }

        public int CheckLogin(string userName, string password)
        {
            password = password.ToMd5();
            List<UserBModel?>? userList = UserDal.GetUserByUserNameAndPassword(userName, password);
            if (userList?.Count <= 0 || userList == null)
            {
                return -1;
            }

            // 查出非删除的用户
            UserBModel? user = userList.Find(user => user?.IsDeleted == false);
            // return user?.Id ?? -1;
            if (user == null)
            {
                return -1;
            }

            UserDal.UpdateLastLoginTime(user.Id);
            return user.Id;
        }

        public string GenerateAutoLoginToken(int userId)
        {
            return UserDal.GenerateAutoLoginToken(userId);
        }

        public int CheckAutoLoginToken(string token)
        {
            int userId = UserDal.CheckAutoLoginToken(token);
            if (userId > 0)
            {
                UserDal.UpdateLastLoginTime(userId);
            }
            return userId;
        }

        public int AddUser(UserBModel userBModel)
        {
            if (userBModel.Password == null)
            {
                return -1;
            }
            // 将密码转为md5
            userBModel.Password = userBModel.Password.ToMd5();
            return UserDal.AddUser(userBModel);
        }

        public string UpdateUser(UserBModel userBModel)
        {
            if (userBModel.Password != null)
            {
                userBModel.Password = userBModel.Password.ToMd5();
            }
            return UserDal.UpdateUser(userBModel) > 0 ? "更新成功" : "更新失败";
        }

        public string RemoveUser(int id)
        {
            return UserDal.RemoveUser(id) > 0 ? "删除成功" : "删除失败";
        }
    }
}
