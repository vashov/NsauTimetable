using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.Models.AccountController
{
    public class SignInUserViewModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
