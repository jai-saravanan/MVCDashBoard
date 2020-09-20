using Domain.Models;
using Domain.Models.Sales;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDashBoard.Services.Interface
{
    public class SalesDashboardService : ISalesDashboardService
    {

        SalesDashboard _oracleConnectionClient = new SalesDashboard();
        public List<SalesDashBoard> SalesPageFirstGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.SalesPageFirstGrid(fromData, toDate, unitYear);
        }

        public List<OpeningBalanceData> GetSalesGridOpeningBalance(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetSalesGridOpeningBalance(fromData, toDate, unitYear);
        }

        public List<ProductWiseSalesReport> GetProductWiseReport(DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetProductWiseReport(toDate, unitYear);
        }

        public List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.ThirdGridGetProductWiseReport(toDate, unitYear);
        }

        public List<FourthGridSalesReport> FourthGridGetProductWiseReport(DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.FourthGridGetProductWiseReport(toDate, unitYear);
        }
    }
}