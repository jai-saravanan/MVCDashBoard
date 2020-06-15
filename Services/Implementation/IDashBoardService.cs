using System;

namespace MVCDashBoard.Services.Implementation
{
    public interface IDashBoardService
    {
        decimal GetSaleTarget(string accountNumber, string unitYear);

        decimal GetTotalOrder(DateTime fromData, DateTime toDate, string unitYear);

        decimal GetTotalSale(DateTime fromData, DateTime toDate, string unitYear);

        decimal GetTotalRecoveryTarget(DateTime fromData, DateTime toDate, string unitYear);

        decimal GetTotalRecoveryReceived(DateTime fromData, DateTime toDate, string unitYear);

        decimal GetTotalCashInHand(DateTime fromData, DateTime toDate, string accountNumber, string unitYear);

        decimal GetTotalCashInBanks(DateTime fromData, DateTime toDate, string accountNumber, string unitYear);

        decimal GetTotalPurchaseOrder(DateTime fromData, DateTime toDate, string unitYear);

        decimal GetTotalPurchase(DateTime fromData, DateTime toDate, string unitYear);
    }
}