using System.Collections.Generic;

namespace Domain.Models
{
    public class PartyWiseRecoveryOutterInfo
    {
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string RecoveryTarget   { get; set; }

        public string PaymentReceived { get; set; }

        public string RemainingRecovery { get; set; }

        public List<PartyWiseRecoveryInnerInfo> InnerInfo { get; set; }

    }


    public class PartyWiseRecoveryInnerInfo
    {
        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string GDate { get; set; }

        public string Particular { get; set; }

        public string Credit { get; set; }
    }
}
        