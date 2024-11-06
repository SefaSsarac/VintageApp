using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageApp.Business.Operations.User.Dtos;
using VintageApp.Business.Types;

namespace VintageApp.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);  

        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}
