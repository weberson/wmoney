[assembly: WebActivator.PostApplicationStartMethod(typeof(WMoney.WebApi.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace WMoney.WebApi.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using WMoney.Core;
    using WMoney.Persistence.EntityFramework;
    
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.RegisterWebApiRequest<WMoneyContext, WMoneyContext>();
            container.RegisterWebApiRequest<IUserCore, UserCore>();
        }
    }
}