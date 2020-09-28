using Domain.Models.Purchase;
using Domain.Models.Sales;
using MVCDashBoard.Services.Implementation;
using Persistance;
using System;
using System.Collections.Generic;

namespace MVCDashBoard.Services.Interface
{
    public class PurchaseDashboardService : IPurchaseDashboardService
    {
        PurchaseDashboard _oracleConnectionClient = new PurchaseDashboard();

        public List<FourthGridSalesReport> FourthGridGetProductWiseReport(int year, string unitYear)
        {
            return _oracleConnectionClient.FourthGridGetProductWiseReport(year, unitYear);
        }

        public List<ProductWiseSalesReport> GetProductWiseReport(int year, string unitYear)
        {
            return _oracleConnectionClient.GetProductWiseReport(year, unitYear);
        }

        public List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(int year, string unitYear)
        {
            return _oracleConnectionClient.ThirdGridGetProductWiseReport(year, unitYear);
        }

        public List<PurchaseOpeningBalanceData> GetOpeningBalance(DateTime fromDate, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetOpeningBalance(fromDate, toDate, unitYear);
        }

        public List<PurchaseTotalPaymentData> GetTotalPayment(DateTime fromDate, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalPayment(fromDate, toDate, unitYear);
        }
    }
}