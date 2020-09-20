
using Domain.Models;
using Domain.Models.Sales;
using Persistance;
using System;
using System.Collections.Generic;

namespace MVCDashBoard.Services.Interface
{
    public interface ISalesDashboardService
    {
        List<SalesDashBoard> SalesPageFirstGrid(DateTime fromData, DateTime toDate, string unitYear);

        List<OpeningBalanceData> GetSalesGridOpeningBalance(DateTime fromData, DateTime toDate, string unitYear);

        List<ProductWiseSalesReport> GetProductWiseReport(DateTime toDate, string unitYear);

        List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(DateTime toDate, string unitYear);

        List<FourthGridSalesReport> FourthGridGetProductWiseReport(DateTime toDate, string unitYear);
    }
}