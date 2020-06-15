﻿using MVCDashBoard.Models;
using MVCDashBoard.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDashBoard.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        IDashBoardService _dashBoardService;
        SessionInfo _sessionInfo;

        public DashboardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;

        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDashBoardData()
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-4);
            var toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddMonths(-4);
            var totalSaleTarget = _dashBoardService.GetSaleTarget("1007", _sessionInfo.UnitYear);
            var totalOrder = _dashBoardService.GetTotalOrder(fromDate, toDate, _sessionInfo.UnitYear);
            var totalSale = _dashBoardService.GetTotalSale(fromDate, toDate, _sessionInfo.UnitYear);
            var totalRecoveryTarget = _dashBoardService.GetTotalRecoveryTarget(fromDate, toDate, _sessionInfo.UnitYear);
            var recoveryReceived = _dashBoardService.GetTotalRecoveryReceived(fromDate, toDate, _sessionInfo.UnitYear);
            var cashInHand = _dashBoardService.GetTotalCashInHand(fromDate, toDate, "24010001", _sessionInfo.UnitYear);
            var cashInBank = _dashBoardService.GetTotalCashInBanks(fromDate, toDate, "24010001", _sessionInfo.UnitYear);
            var totalPurchaseOrder = _dashBoardService.GetTotalPurchaseOrder(fromDate, toDate, _sessionInfo.UnitYear);
            var totalPurchase = _dashBoardService.GetTotalPurchase(fromDate, toDate, _sessionInfo.UnitYear);

            var remainingTarget = totalSaleTarget - totalOrder;
            var remainingRecovery = totalRecoveryTarget - recoveryReceived;
            var totalCashAndBanks = cashInHand + cashInBank;
            var remainingPurchase = totalPurchaseOrder - totalPurchase;



            return Json(new
            {
                totalSaleTarget = decimal.Round(totalSaleTarget),
                totalOrder = decimal.Round(totalOrder),
                totalSale = decimal.Round(totalSale),
                remainingTarget = decimal.Round(remainingTarget),
                totalRecoveryTarget = decimal.Round(totalRecoveryTarget),
                recoveryReceived = decimal.Round(recoveryReceived),
                remainingRecovery = decimal.Round(remainingRecovery),
                cashInHand = decimal.Round(cashInHand),
                cashInBank = decimal.Round(cashInBank),
                totalCashAndBanks = decimal.Round(totalCashAndBanks),
                totalPurchaseOrder = decimal.Round(totalPurchaseOrder),
                totalPurchase = decimal.Round(totalPurchase),
                remainingPurchase = decimal.Round(remainingPurchase)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDashBoardPartyWiseSalesData()
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-4);
            var toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddMonths(-4);


            var outerData = _dashBoardService.PartyWiseSaleOuterGrid(fromDate, toDate, _sessionInfo.UnitYear);
            var innerData = _dashBoardService.PurchaseWiseSaleInnerGrid(fromDate, toDate, _sessionInfo.UnitYear);

            return Json(new { data = outerData }, JsonRequestBehavior.AllowGet);
        }

    }
}