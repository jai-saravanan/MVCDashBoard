using System.Collections.Generic;

namespace Domain.Models
{
    public class ExpencesOutterInfo
    {
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ExpenceTarget { get; set; }

        public string TotalExpence { get; set; }

        public string RemainingExpence { get; set; }

        public List<ExpencesInnerInfo> InnerInfo { get; set; }
    }

    public class ExpencesInnerInfo
    {
        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string GDate { get; set; }

        public string Particular { get; set; }

        public string Credit { get; set; }
    }
}
