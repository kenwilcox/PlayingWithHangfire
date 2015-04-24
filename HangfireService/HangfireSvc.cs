using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace HangfireService
{
  public partial class HangfireSvc : ServiceBase
  {
    private BackgroundJobServer _server;

    public HangfireSvc()
    {
      InitializeComponent();
      GlobalConfiguration.Configuration.UseSqlServerStorage("Hangfire");
    }

    protected override void OnStart(string[] args)
    {
      _server = new BackgroundJobServer(new BackgroundJobServerOptions()
      {
        WorkerCount = Environment.ProcessorCount * 5,

      });
    }

    protected override void OnStop()
    {
      _server.Dispose();
    }
  }
}
