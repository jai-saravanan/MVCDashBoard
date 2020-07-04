using System.Collections.Generic;

namespace Domain.Models
{
    public class PartyWisePurchaseOutterInfo
    {
        public string ProductCode { get; set; }
        public string ProductName    { get; set; }
        public string TotalPurchaseOrder  { get; set; }
        public string TotalPurchase  { get; set; }
        public string RemainingPurchase { get; set; }

        public List<string> OrderDetail { get; set; }
    }

    public class PartyWisePurchaseInnerInfo
    {
        public string ProductName { get; set; }

        public string OrderDetail { get; set; }
    }
}
