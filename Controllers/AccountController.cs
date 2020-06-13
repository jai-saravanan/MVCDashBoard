using MVCDashBoard.Models;
using MVCDashBoard.Services.Interface;
using Domain.ViewModel;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCDashBoard.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel();
            userLoginViewModel.CompanyInfo = _accountService.GetCompanyDetails();
            return View(userLoginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel userLoginViewModel)
        {
            var isValid = _accountService.ValidateUser(userLoginViewModel);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(userLoginViewModel.UserName, false);
                Session["UserInfo"] = new SessionInfo()
                {
                    UserName = userLoginViewModel.UserName,
                    UnitYear = userLoginViewModel.CompanyId,
                    CompanyName = userLoginViewModel.CompanyName
                };
                return RedirectToAction("Index", "Dashboard");
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserInfo"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}