using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDashBoard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTotalAmt()
        {
            int[] vars = new int[5];

            vars[0] = 12;

            vars[1] = 4;


            int totalAmt = 0;
            for (int i = 0; i < vars.Length; i++)
            {

                totalAmt = totalAmt + vars[i];
            }

            return Json(totalAmt, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}