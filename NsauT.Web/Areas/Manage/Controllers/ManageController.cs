using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NsauT.Web.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "manager,admin")]
    public class ManageController : Controller
    {
    }
}