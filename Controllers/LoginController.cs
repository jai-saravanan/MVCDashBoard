using MVCDashBoard.ViewModel;
using System.Web.Mvc;

namespace MVCDashBoard.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel userLoginViewModel)
        {


            return View();
        }

    }
}