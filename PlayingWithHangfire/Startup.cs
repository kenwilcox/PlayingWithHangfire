using Microsoft.Owin;
using Owin;
using Hangfire;
using Microsoft.Practices.Unity;

[assembly: OwinStartupAttribute(typeof(PlayingWithHangfire.Startup))]

namespace PlayingWithHangfire
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);

      //If you are processing your jobs inside an ASP.NET application, you should be warned about some setting that may prevent your scheduled jobs to be performed in-time. To avoid that behavour, perform the following steps:
      //Disable Idle Timeout – set its value to 0.
      //Use the application auto-start feature.
      //GlobalConfiguration.Configuration.UseSqlServerStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
      GlobalConfiguration.Configuration.UseSqlServerStorage("Hangfire");
      app.UseHangfireDashboard();
      app.UseHangfireServer();

      //app.UseHangfire(config =>
      //{
      //  var container = new UnityContainer();
      //  config.UseUnityActivator(container);
      //});
    }
  }
}
