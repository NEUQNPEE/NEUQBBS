using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entities.BModels;

namespace WebapiDemo.Models
{
    public class UserUpdateRequest
    {
        public required int Id { get; set; }
        public string? Password { get; set; }
        public string? NickName { get; set; }
        public string? Gender { get; set; }
        public string? Signature { get; set; }
        public string? Avatar { get; set; }
    }

    public static class UserUpdateVToBMapper
    {
        public static UserBModel ToBModel(this UserUpdateRequest request)
        {
            return new UserBModel
            {
                Id = request.Id,
                Password = request.Password,
                NickName = request.NickName,
                Gender = request.Gender,
                Signature = request.Signature,
                Avatar = request.Avatar
            };
        }
    }
}
