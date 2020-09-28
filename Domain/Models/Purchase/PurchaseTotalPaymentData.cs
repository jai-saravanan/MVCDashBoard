namespace Domain.Models.Purchase
{
    public class PurchaseTotalPaymentData
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string UnitYear { get; set; }
        public decimal TotalPayment { get; set; }
    }
}
