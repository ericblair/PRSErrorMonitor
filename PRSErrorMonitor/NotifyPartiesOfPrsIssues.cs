using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace PRSErrorMonitor
{
    public class NotifyPartiesOfPrsIssues
    {
        ISMTPWrapper _smtpClient;
 
        public NotifyPartiesOfPrsIssues()

        public void SendEmailToHelpdesk(bool prsUnavailableErrorLimitReached, bool prsTimeoutErrorLimitReached, bool prsTotalErrorLimitReached)
        {
            MailMessage email = ComposeEmail(prsUnavailableErrorLimitReached, prsTimeoutErrorLimitReached, prsTotalErrorLimitReached);
        }

        private MailMessage ComposeEmail(bool prsUnavailableErrorLimitReached, bool prsTimeoutErrorLimitReached, bool prsTotalErrorLimitReached)
        {
            MailMessage email = new MailMessage();
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"]);
            email.From = from;
            MailAddress to = new MailAddress(ConfigurationManager.AppSettings["EmailToAddress"]);
            email.To.Add(to);
            MailAddress cc = new MailAddress(ConfigurationManager.AppSettings["EmailCCAddresses"]);
            email.CC.Add(cc); // may have to review this......
            MailAddress replyTo = new MailAddress(ConfigurationManager.AppSettings["EmailReplyToAddress"]);
            email.ReplyTo = replyTo;    // Consider using replyToList....
            email.Subject = ConfigurationManager.AppSettings["EmailSubject"];

            if ((prsUnavailableErrorLimitReached == false) && (prsTotalErrorLimitReached == false))
            {
                // Total limit has been reached
                email.Body = ConfigurationManager.AppSettings["TotalErrorLimitExceededBodyText"];
                return email;
            }

            string _bodyText = null;

            if (prsUnavailableErrorLimitReached == true)
            {
                _bodyText += ConfigurationManager.AppSettings["UnavailableErrorLimitExceededBodyText"];
            }
            if (prsTotalErrorLimitReached == true)
            {
                _bodyText += ConfigurationManager.AppSettings["TimeoutErrorLimitExceededBodyText"];
            }

            email.Body = _bodyText;

            return email;
        }
    }
}
