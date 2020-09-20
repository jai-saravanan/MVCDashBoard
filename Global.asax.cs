using Autofac;
using Autofac.Integration.Mvc;
using MVCDashBoard.Services.Implementation;
using MVCDashBoard.Services.Interface;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCDashBoard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacRegistration.BuildContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class AutofacRegistration
    {
        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Now grab your connection string and wire up your db context
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<DashBoardService>().As<IDashBoardService>();
            builder.RegisterType<SalesDashboardService>().As<ISalesDashboardService>();
            

            // You can register any other dependencies here

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
