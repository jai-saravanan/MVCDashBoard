using Domain.Models;
using MVCDashBoard.Models;
using MVCDashBoard.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDashBoard.Controllers
{
    public class PurchaseDashboardController : Controller
    {
        IDashBoardService _dashBoardService;
        IPurchaseDashboardService _purchaseDashboardService;
        SessionInfo _sessionInfo;

        public PurchaseDashboardController(IDashBoardService dashBoardService, IPurchaseDashboardService purchaseDashboardService)
        {
            _dashBoardService = dashBoardService;
            _purchaseDashboardService = purchaseDashboardService;
        }

        public ActionResult PurchaseDashBoard()
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            return View(model: _sessionInfo.UnitYear);
        }

        // GET: PurchaseDashboard
        public JsonResult GetDashBoardPartyWisePurchaseData(DateTime fromDate, DateTime toDate)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;

            // first grid
            var outerData2ndGrid = _dashBoardService.PartyWisePurchaseOuterGrid(fromDate, toDate, _sessionInfo.UnitYear);
            var innerData2ndGrid = _dashBoardService.PartyWisePurchaseInnerGrid(fromDate, toDate, _sessionInfo.UnitYear);

            foreach (var item in outerData2ndGrid)
            {
                var orderDetail = innerData2ndGrid?.Where(x => x.Name == item.ProductName)?.OrderBy(x => x.ProductName).ToList();
                if (orderDetail != null || orderDetail.Count() > 0)
                {
                    item.OrderDetail = new List<PartyWisePurchaseInnerInfo>();
                    item.OrderDetail.AddRange(orderDetail);
                }

            }
            return Json(new { data = outerData2ndGrid.OrderBy(x => x.ProductName) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPartyWisePurchaseGrid(int year)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _purchaseDashboardService.GetProductWiseReport(year, _sessionInfo.UnitYear);


            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Name) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductWisePurchaseGridData(int year)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _purchaseDashboardService.ThirdGridGetProductWiseReport(year, _sessionInfo.UnitYear);


            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Description) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClaimreturnStockGridData(int year)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _purchaseDashboardService.FourthGridGetProductWiseReport(year, _sessionInfo.UnitYear);


            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Name) }, JsonRequestBehavior.AllowGet);
        }


    }
}