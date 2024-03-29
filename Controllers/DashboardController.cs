﻿using Domain.Models;
using MVCDashBoard.Models;
using MVCDashBoard.Services.Implementation;
using MVCDashBoard.Services.Interface;
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
        ISalesDashboardService _salesDashboardService;
        SessionInfo _sessionInfo;

        public DashboardController(IDashBoardService dashBoardService, ISalesDashboardService salesDashboardService)
        {
            _dashBoardService = dashBoardService;
            _salesDashboardService = salesDashboardService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalesDashBoard()
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            return View(model: _sessionInfo.UnitYear);
        }

        public JsonResult GetDashBoardData(DateTime fromDate, DateTime toDate)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
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

        public JsonResult GetDashBoardPartyWiseSalesData(DateTime fromDate, DateTime toDate)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;

            // first grid
            var outerData = _dashBoardService.PartyWiseSaleOuterGrid(fromDate, toDate, _sessionInfo.UnitYear);
            var innerData = _dashBoardService.PartyWiseSaleInnerGrid(fromDate, toDate, _sessionInfo.UnitYear);

            foreach (var item in outerData)
            {
                var orderDetail = innerData?.Where(x => x.Name == item.Name)?.Select(x => x.Detail)?.OrderByDescending(x => x);
                if (orderDetail != null || orderDetail.Count() > 0)
                {
                    item.OrderDetail = new List<string>();
                    item.OrderDetail.AddRange(orderDetail);
                }

            }

            return Json(new { data = outerData.OrderBy(x => x.Name) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDashBoardPartyWiseRecoveryData(DateTime fromDate, DateTime toDate)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;

            // first grid
            var outerData2ndGrid = _dashBoardService.PartyWiseRecoveryOutterGrid(fromDate, toDate, _sessionInfo.UnitYear);
            var innerData2ndGrid = _dashBoardService.PartyWiseRecoveryInnerGrid(fromDate, toDate, _sessionInfo.UnitYear);

            foreach (var item in outerData2ndGrid)
            {
                var orderDetail = innerData2ndGrid?.Where(x => x.Name == item.ProductName)?.OrderByDescending(x => x.GDate).ToList();
                if (orderDetail != null || orderDetail.Count() > 0)
                {
                    item.InnerInfo = new List<PartyWiseRecoveryInnerInfo>();
                    item.InnerInfo.AddRange(orderDetail);
                }

            }
            return Json(new { data = outerData2ndGrid.OrderBy(x => x.ProductName) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDashBoardExpensesData(DateTime fromDate, DateTime toDate)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;

            var outerData2ndGrid = _dashBoardService.ExpencesOutterGrid(fromDate, toDate, _sessionInfo.UnitYear);
            var innerData2ndGrid = _dashBoardService.ExpencesInnerGrid(fromDate, toDate, _sessionInfo.UnitYear);

            foreach (var item in outerData2ndGrid)
            {
                var orderDetail = innerData2ndGrid?.Where(x => x.Name == item.ProductName)?.OrderByDescending(x => x.GDate).ToList();
                if (orderDetail != null || orderDetail.Count() > 0)
                {
                    item.InnerInfo = new List<ExpencesInnerInfo>();
                    item.InnerInfo.AddRange(orderDetail);
                }

            }
            return Json(new { data = outerData2ndGrid.OrderBy(x => x.ProductName) }, JsonRequestBehavior.AllowGet);
        }

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

        public JsonResult GetSalesDashBoardPartyWiseSale(DateTime fromDate, DateTime toDate)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _salesDashboardService.SalesPageFirstGrid(fromDate, toDate, _sessionInfo.UnitYear);
            var innerData2ndGrid = _salesDashboardService.GetSalesGridOpeningBalance(fromDate, toDate, _sessionInfo.UnitYear);

            foreach (var item in outerData2ndGrid)
            {
                item.OpeningBalance = (decimal.Round(innerData2ndGrid?.Where(x => x.Name == item.Name)?.Sum(x => x.OpeningBalance) ?? 0)).ToString();
            }
            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Name) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalesDashBoardProductWiseSale(int year)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _salesDashboardService.GetProductWiseReport(year, _sessionInfo.UnitYear);


            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Name) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ThirdGridGetSalesDashBoard(int year)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _salesDashboardService.ThirdGridGetProductWiseReport(year, _sessionInfo.UnitYear);


            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Description) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FourthGridGetSalesDashBoard(int year)
        {
            _sessionInfo = Session["UserInfo"] as SessionInfo;
            // first grid
            var outerData2ndGrid = _salesDashboardService.FourthGridGetProductWiseReport(year, _sessionInfo.UnitYear);


            return Json(new { data = outerData2ndGrid.OrderBy(x => x.Name) }, JsonRequestBehavior.AllowGet);
        }

    }


}