using Owin;
using System.Web.Http;

namespace KatanaHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Web.API configs
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("bugs", "api/{Controller}");

            app.UseWebApi(config);

            app.MapSignalR();

            // Nancy configs
            app.UseNancy();
        }
    }
}