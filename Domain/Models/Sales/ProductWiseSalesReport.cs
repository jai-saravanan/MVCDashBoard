namespace Domain.Models.Sales
{
    public class ProductWiseSalesReport : Base
    {
        public string Name { get; set; }
    }

    public class ThirdGridSalesReport : Base
    {
        public string Description { get; set; }
    }

    public class FourthGridSalesReport : ProductWiseSalesReport
    {
    }

    public class Base
    {

        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public decimal Total { get; set; }
    }
}
