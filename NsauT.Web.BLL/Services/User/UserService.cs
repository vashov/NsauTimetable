using Microsoft.AspNetCore.Identity;
using NsauT.Web.BLL.Services.User.DTO;
using NsauT.Web.DAL.Models;
using System.Threading.Tasks;

namespace NsauT.Web.BLL.Services.User
{
    public class UserService : IUserService
    {
        private SignInManager<UserEntity> SignInManager { get; }

        public UserService(SignInManager<UserEntity> signInManager)
        {
            SignInManager = signInManager;
        }

        public async Task<ServiceResult> SignInAsync(SignInUserDto signInUserDto)
        {
            SignInResult signInResult = await SignInManager
                .PasswordSignInAsync(signInUserDto.Login, signInUserDto.Password, signInUserDto.RememberMe, false);

            if (signInResult.Succeeded)
            {
                return new ServiceResult(Result.OK);
            }

            var serviceResult = new ServiceResult(Result.Error);
            var error = ("", "Неверный логин и (или) пароль");
            serviceResult.Errors.Add(error);
            return serviceResult;
        }
    }
}
