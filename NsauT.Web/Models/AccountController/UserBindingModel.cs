using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.Models.AccountController
{
    public class UserBindingModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
