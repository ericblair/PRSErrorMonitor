using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace PRSErrorMonitor
{
    public class SMTPWrapper : ISMTPWrapper
    {
        private SmtpClient _client;

        // Unit testing
        public SMTPWrapper() { }

        public SMTPWrapper(SmtpClient smtpclient)
        {
            _client = smtpclient;
        }

        public SmtpClient ConfigureSmtpClient()
        {
            _client = new SmtpClient();

            return _client;
        }

        public void Send(MailMessage msg)
        {
            _client.Send(msg);
        }
    }
}
