

using Domain.Models.Sales;
using System.Collections.Generic;

namespace MVCDashBoard.Services.Implementation
{
    public interface IPurchaseDashboardService
    {
        List<ProductWiseSalesReport> GetProductWiseReport(int year, string unitYear);

        List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(int year, string unitYear);

        List<FourthGridSalesReport> FourthGridGetProductWiseReport(int year, string unitYear);
    }
}