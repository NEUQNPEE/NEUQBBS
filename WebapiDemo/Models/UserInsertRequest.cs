using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entities.BModels;

namespace WebApiDemo.Models
{
    public class UserInsertRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? NickName { get; set; }
        public string? Gender { get; set; }
        public string? Signature { get; set; }
        public string? Avatar { get; set; }
    }

    public static class UserInsertVToBMapper
    {
        public static UserBModel ToBModel(this UserInsertRequest request)
        {
            return new UserBModel
            {
                UserName = request.UserName,
                Password = request.Password,
                NickName = request.NickName,
                Gender = request.Gender,
                Signature = request.Signature,
                Avatar = request.Avatar
            };
        }
    }
}
