
using MVCDashBoard.Services.Interface;
using Domain.ViewModel;
using Persistance;
using System.Collections.Generic;

namespace MVCDashBoard.Services.Implementation
{
    public class AccountService : IAccountService
    {
        OracleConnectionClient _oracleConnectionClient = new OracleConnectionClient();

        public List<CompanyInfo> GetCompanyDetails()
        {
            return _oracleConnectionClient.GetCompanyDetails();
        }

        public bool ValidateUser(UserLoginViewModel userLoginViewModel)
        {
            return _oracleConnectionClient.ValidateUserLogin(userLoginViewModel.UserName, userLoginViewModel.Password, userLoginViewModel.CompanyId);
        }
    }
}