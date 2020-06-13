using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class UserLoginViewModel
    {
        public string CompanyId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<CompanyInfo> CompanyInfo { get; set; }

        public string CompanyName { get; set; }
    }

    public class CompanyInfo
    {
        public string UnitYear { get; set; }

        public string CompanyName { get; set; }
    }
}