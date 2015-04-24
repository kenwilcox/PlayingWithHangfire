using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Workers
{
  public delegate void WorkerDelegate();

  public partial class Worker
  {
    private static Worker _worker = new Worker();
    private Random _random = new Random();
    private static int attempts = 0;

    #region Static Methods

    public static void FireAndForget()
    {
      BackgroundJob.Enqueue(() => _worker.FireAndForget(100));
    }

    public static void FireAndForgetDelay(int delay)
    {
      BackgroundJob.Schedule(() => _worker.FireAndForget(100), TimeSpan.FromMinutes(delay));
    }

    public static void Recurring(Func<string> cron)
    {
      if (cron == Cron.Minutely) _worker.RecurringMinutely();
      else throw new Exception("I can't do that yet!");
    }

    public static void Continuations()
    {
      var id = BackgroundJob.Enqueue(() => _worker.FirstContinuation());
      BackgroundJob.ContinueWith(id, () => _worker.SecondContinuation());
    }

    public static void Continuations(WorkerDelegate first, WorkerDelegate second)
    {
      var id = BackgroundJob.Enqueue(() => first());
      BackgroundJob.ContinueWith(id, () => second());
    }

    public static void Continuations(params WorkerDelegate[] actions)
    {
      _worker.AllMyContinuations(actions);
    }

    public static bool FailRandomly()
    {
      var count = _worker._random.Next(10, 20);
      //var fail = count < 15;
      _worker.RandomTask(count);
      return count < 15;
    }

    public static void ScheduleTask()
    {
      //RecurringJob.AddOrUpdate(Guid.NewGuid().ToString(), () => _worker.FireAndForget(10), "30 0 1-31/2 * *");
      RecurringJob.AddOrUpdate(() => _worker.FireAndForget(10), "30 0 1-31/2 * *");
    }
    
    public static void TriggerTask(string id)
    {
      RecurringJob.Trigger(id);
    }

    public static void JobWithCancelToken()
    {
      //_worker.LongRunningMethod(JobCancellationToken.Null);
      BackgroundJob.Enqueue(() => _worker.LongRunningMethod(JobCancellationToken.Null));
    }

    public static void EmailSender(int userId, string message)
    {
      BackgroundJob.Enqueue<EmailSender>(x => x.Send(userId, message));
    }

  #endregion

  }
}
