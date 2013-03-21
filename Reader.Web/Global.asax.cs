namespace Reader.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Reader.Services;
    
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Configuration.Initialize(System.Configuration.ConfigurationManager.AppSettings);
        }
    }
}