using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;

namespace HangfireConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      //GlobalConfiguration.Configuration.UseSqlServerStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
      LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
      BackgroundJob.Enqueue(() => DoSomething());
    }

    static void DoSomething()
    {
      Console.WriteLine("I'm going to crash");
      throw new Exception("See, told ya!");
    }
  }
}
