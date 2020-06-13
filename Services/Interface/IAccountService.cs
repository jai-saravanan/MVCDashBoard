using Domain.ViewModel;
using System.Collections.Generic;

namespace MVCDashBoard.Services.Interface
{
    public interface IAccountService
    {
        bool ValidateUser(UserLoginViewModel userLoginViewModel);

        List<CompanyInfo> GetCompanyDetails();
    }
}
