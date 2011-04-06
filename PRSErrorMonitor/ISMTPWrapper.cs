using System;

namespace PRSErrorMonitor
{
    public interface ISMTPWrapper
    {
        System.Net.Mail.SmtpClient ConfigureSmtpServer();
        void Send(System.Net.Mail.MailMessage msg);
    }
}
