using System.Collections.Generic;

namespace Domain.Models
{
    public class PartyWiseSaleOuterInfo
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string SalesTarget { get; set; }
        public string TotalOrder { get; set; }
        public string TotalSale { get; set; }
        public string RemainingSale { get; set; }

        public List<string> OrderDetail { get; set; }
    }

    public class PartyWiseSaleInnerInfo
    {
        public string Name { get; set; }

        public string Detail { get; set; }
    }
}
