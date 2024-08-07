using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL;
using WebApiDemo.Entities.BModels;
using WebApiDemoCommon;

namespace WebApiDemo.BLL
{
    /// <summary>
    /// 用户相关业务逻辑的实现
    /// </summary>
    public class UserBll : IUserBll
    {
        /// <summary>
        /// debug 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<UserBModel>? DebugGetAllUsers()
        {
            return UserDal.DebugGetAllUsers();
        }

        /// <summary>
        /// debug 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserBModel? DebugGetUserById(int id)
        {
            return UserDal.DebugGetUserById(id);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserBModel? GetUserById(int id)
        {
            return UserDal.GetUserById(id);
        }

        /// <summary>
        /// 根据用户ID获取用户名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserNameById(int id)
        {
            UserBModel? user = UserDal.GetUserById(id);
            if (user == null)
            {
                return "未找到";
            }

            return user.UserName ?? "用户名空引用！";
        }

        /// <summary>
        /// 根据用户ID列表获取用户名列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<string> GetUserNamesByIds(List<int> ids)
        {
            List<string> userNames = new();
            foreach (int id in ids)
            {
                userNames.Add(GetUserNameById(id));
            }

            return userNames;
        }

        /// <summary>
        /// 根据用户ID获取用户用于POST的数据，只保留userName,RigisterTime和Points字段，注意排除IsDeleted字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserBModel? GetUserByIdForPost(int id)
        {
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

        /// <summary>
        /// 根据用户ID获取用户基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 根据用户ID获取用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserBModel? GetUserDetailInfoById(int id)
        {
            return UserDal.GetUserById(id);
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserExist(string userName)
        {
            return UserDal.CheckUserExist(userName);
        }

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int CheckLogin(string userName, string password)
        {
            password = password.ToMd5();
            List<UserBModel>? userList = UserDal.GetUserByUserNameAndPassword(userName, password);
            if (userList?.Count <= 0 || userList == null)
            {
                return -1;
            }

            // 查出非删除的用户
            UserBModel? user = userList.Find(user => user?.IsDeleted == false);
            if (user == null)
            {
                return -1;
            }

            UserDal.UpdateLastLoginTime(user.Id);
            return user.Id;
        }

        /// <summary>
        /// 生成自动登录token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GenerateAutoLoginToken(int userId)
        {
            return UserDal.GenerateAutoLoginToken(userId);
        }

        /// <summary>
        /// 检查自动登录token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int CheckAutoLoginToken(string token)
        {
            int userId = UserDal.CheckAutoLoginToken(token);
            if (userId > 0)
            {
                UserDal.UpdateLastLoginTime(userId);
            }
            return userId;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userBModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userBModel"></param>
        /// <returns></returns>
        public string UpdateUser(UserBModel userBModel)
        {
            if (userBModel.Password != null)
            {
                userBModel.Password = userBModel.Password.ToMd5();
            }
            return UserDal.UpdateUser(userBModel) > 0 ? "更新成功" : "更新失败";
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemoveUser(int id)
        {
            return UserDal.RemoveUser(id) > 0 ? "删除成功" : "删除失败";
        }
    }
}
