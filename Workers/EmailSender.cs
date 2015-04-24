using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
  public class EmailSender
  {
    private int _userId;
    private string _message;

    // Job activator needs a default constructor (no args)
    public EmailSender()
    {
      _userId = -1;
      _message = "";
    }

    public void Send(int userId, string message)
    {
      // Send an email to userId with the message
      System.Threading.Thread.Sleep(1000*10);
    }
  }
}
