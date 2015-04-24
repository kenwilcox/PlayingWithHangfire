using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
  public class EmailSender
  {
    public void Send(int userId, string message)
    {
      // Send an email to userId with the message
      System.Threading.Thread.Sleep(1000*10);
    }
  }
}
