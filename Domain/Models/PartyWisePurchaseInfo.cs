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

        public List<PartyWisePurchaseInnerInfo> OrderDetail { get; set; }

        public string OpeningBalance { get; set; }

        public string TotalPayment { get; set; }

        public string CurrentBalance { get; set; }
    }
            
    public class PartyWisePurchaseInnerInfo
    {
        public string Name { get; set; }    

        public string ProductName { get; set; }

        public string Rate { get; set; }
        public string Orders { get; set; }
        public string Received { get; set; }
    }
}
