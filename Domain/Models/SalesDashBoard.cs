using System.Collections.Generic;

namespace Domain.Models
{
    public class SalesDashBoard
    {
        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string TotalSale { get; set; }

        public string PaymentReceived { get; set; }
        public string Salary { get; set; }
        public string ZonalExpense { get; set; }
        public string TotalRecovery { get; set; }
        public string CLAI { get; set; }
        public string Transfer { get; set; }

        public string OpeningBalance { get; set; }
    }


    public class OpeningBalanceData
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal OpeningBalance { get; set; }
    }
}
