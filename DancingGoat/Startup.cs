using System.Web.Mvc;
using DancingGoat.Infrastructure;
using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

[assembly: OwinStartup(typeof(DancingGoat.Startup))]

namespace DancingGoat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // DI
            var container = new Container();
            container.Register<IProjectContext, ProjectIdFromUrlContext>(Lifestyle.Transient);
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}