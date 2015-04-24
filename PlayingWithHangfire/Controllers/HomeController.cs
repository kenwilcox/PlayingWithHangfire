﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangfire;
using Workers;

namespace PlayingWithHangfire.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    public ActionResult FireAndForget()
    {
      Worker.FireAndForget();
      return Redirect("/");
    }

    public ActionResult RecurringMinutely()
    {
      Worker.Recurring(Cron.Minutely);
      return Redirect("/");
    }

    public ActionResult Continuations()
    {
      Worker.Continuations();
      return Redirect("/");
    }

    public ActionResult MyContinuations()
    {
      //var worker = new Worker();
      //Worker.Continuations(worker.FirstContinuation, worker.SecondContinuation);
      Worker.Continuations(DoFirstTask, DoSecondTask);
      return Redirect("/");
    }

    public ActionResult FailRandomly()
    {
      if (Worker.FailRandomly()) Response.End();
      return Redirect("/");
    }

    public ActionResult FireAndForgetDelay()
    {
      Worker.FireAndForgetDelay(10);
      return Redirect("/");
    }

    public static void DoFirstTask()
    {
      System.Threading.Thread.Sleep(1000 * 10);
    }

    public static void DoSecondTask()
    {
      System.Threading.Thread.Sleep(1000 * 20);
    }
  }
}