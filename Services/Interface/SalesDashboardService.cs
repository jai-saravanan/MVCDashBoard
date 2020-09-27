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

        public List<ProductWiseSalesReport> GetProductWiseReport(int year, string unitYear)
        {
            return _oracleConnectionClient.GetProductWiseReport(year, unitYear);
        }

        public List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(int year, string unitYear)
        {
            return _oracleConnectionClient.ThirdGridGetProductWiseReport(year, unitYear);
        }

        public List<FourthGridSalesReport> FourthGridGetProductWiseReport(int year, string unitYear)
        {
            return _oracleConnectionClient.FourthGridGetProductWiseReport(year, unitYear);
        }
    }
}