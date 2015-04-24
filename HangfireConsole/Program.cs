using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Hangfire.SqlServer;

namespace HangfireConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      GlobalConfiguration.Configuration.UseSqlServerStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
      //LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
      //BackgroundJob.Enqueue(() => DoSomething());

      //var server = new BackgroundJobServer(options);
      //BackgroundJob.Enqueue(() => DoSomething());
      //server.Dispose();

      using (var server = new BackgroundJobServer())
      {
        BackgroundJob.Enqueue(() => DoSomething());

        Console.WriteLine("Server Started. Press any key to exit...");
        Console.ReadKey();
      }

    }

    public static void DoSomething()
    {
      Console.WriteLine("I'm going to crash");
      //throw new Exception("See, told ya!");
    }
  }
}
