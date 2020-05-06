using Microsoft.AspNetCore.Identity;
using NsauT.Web.BLL.Services.User.DTO;
using NsauT.Web.DAL.Models;
using System.Linq;
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
            if (IsNotNullOrWhitespace(signInUserDto.Login, signInUserDto.Password))
            {
                SignInResult signInResult = await SignInManager
                .PasswordSignInAsync(signInUserDto.Login, signInUserDto.Password, signInUserDto.RememberMe, false);

                if (signInResult.Succeeded)
                {
                    return new ServiceResult(Result.OK);
                }
            }

            var serviceResult = new ServiceResult(Result.Error);
            var error = ("", "Неверный логин и (или) пароль");
            serviceResult.Errors.Add(error);
            return serviceResult;
        }

        public async Task SignOutAsync()
        {
            // удаляем аутентификационные куки
            await SignInManager.SignOutAsync();
        }

        private bool IsNotNullOrWhitespace(params string[] values)
        {
            return values.All(v => !string.IsNullOrWhiteSpace(v));
        }
    }
}
