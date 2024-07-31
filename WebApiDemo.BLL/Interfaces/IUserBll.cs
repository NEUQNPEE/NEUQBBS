using WebApiDemo.Entities;
using WebApiDemo.Entities.BModels;

namespace WebApiDemo.BLL.Interfaces
{
    public interface IUserBll
    {
        List<UserBModel?>? DebugGetAllUsers();

        UserBModel? DebugGetUserById(int id);

        UserBModel? GetUserById(int id);
        UserBModel? GetUserByIdForPost(int id);

        string GetUserNameById(int id);

        List<string> GetUserNamesByIds(List<int> ids);
        
        // 查询是否存在该用户名
        bool CheckUserExist(string userName);

        int CheckLogin(string userName, string password);

        string GenerateAutoLoginToken(int userId);

        int CheckAutoLoginToken(string token);

        int AddUser(UserBModel userBModel);

        string UpdateUser(UserBModel userBModel);

        string RemoveUser(int id);

        UserBModel? GetUserBaseInfoById(int id);

        UserBModel? GetUserDetailInfoById(int id);
    }
}
