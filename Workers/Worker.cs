using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
  public delegate void WorkerDelegate();

  public class Worker
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
      _worker.RandomTaks(count);
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

    public static void EmailSender(int userId, string message)
    {
      BackgroundJob.Enqueue<EmailSender>(x => x.Send(userId, message));
    }

  #endregion

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
      System.Threading.Thread.Sleep(1000 * 30);
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
        System.Threading.Thread.Sleep(1000*seconds);
        //if (shouldIFail) throw new Exception("I failed for some reason");
      }
    }

  }
}
