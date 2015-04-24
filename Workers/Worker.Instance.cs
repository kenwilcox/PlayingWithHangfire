using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Workers
{
  public partial class Worker
  {
    #region Instance Methods
    public void RecurringMinutely()
    {
      RecurringJob.AddOrUpdate(() => Console.WriteLine("Minute Job" + Guid.NewGuid().ToString()), Cron.Minutely);
    }

    public void FireAndForget(int count)
    {
      Parallel.For(0, count, i =>
      {
        Console.WriteLine("Fire-and-forget " + i.ToString());
      });
    }

    public void FirstContinuation()
    {
      Console.WriteLine("Hello, ");
      Thread.Sleep(1000 * 30);
    }

    public void SecondContinuation()
    {
      Console.WriteLine("world!");
    }

    public void AllMyContinuations(WorkerDelegate[] actions)
    {
      if (actions.Length.Equals(0)) throw new Exception("I can't do this with nothing");

      var id = BackgroundJob.Enqueue(() => actions[0]());
      for (var i = 1; i < actions.Length; i++)
      {
        id = BackgroundJob.ContinueWith(id, () => actions[i]());
      }
    }

    public void RandomTaks(int seconds)//, bool shouldIFail)
    {
      attempts++;
      if (attempts < 10)
      {
        Thread.Sleep(1000 * seconds);
        //if (shouldIFail) throw new Exception("I failed for some reason");
      }
    }

    public void LongRunningMethod(IJobCancellationToken jobCancellationToken)
    {
      for (var i = 0; i < Int32.MaxValue; i++)
      {
        jobCancellationToken.ThrowIfCancellationRequested();
        Thread.Sleep(TimeSpan.FromSeconds(1));
      }
    }
    #endregion
  }
}
