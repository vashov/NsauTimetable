using NsauT.Web.BLL.Services.User.DTO;
using System.Threading.Tasks;

namespace NsauT.Web.BLL.Services.User
{
    public interface IUserService
    {
        Task<ServiceResult> SignInAsync(SignInUserDto signInUserDto);
        Task SignOutAsync();
    }
}
