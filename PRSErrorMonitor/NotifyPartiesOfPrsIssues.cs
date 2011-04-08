﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace PRSErrorMonitor
{
    public class NotifyPartiesOfPrsIssues : PRSErrorMonitor.INotifyPartiesOfPrsIssues
    {
        SmtpClient _smtpClient;
        IConfigFileHelper _configFileHelper;
 
        public NotifyPartiesOfPrsIssues()
        {
            _smtpClient = new SmtpClient();
            _configFileHelper = new ConfigFileHelper();
        }

        public void SendEmailToHelpdesk(bool prsUnavailableErrorLimitReached, bool prsTimeoutErrorLimitReached, bool prsTotalErrorLimitReached)
        {
            MailMessage email = ComposeEmail(prsUnavailableErrorLimitReached, prsTimeoutErrorLimitReached, prsTotalErrorLimitReached);
            _smtpClient.Send(email);
            // Set flag to prevent any more emails being sent until flag is reset
            _configFileHelper.EmailSentFlag = 1;
        }

        private MailMessage ComposeEmail(bool prsUnavailableErrorLimitReached, bool prsTimeoutErrorLimitReached, bool prsTotalErrorLimitReached)
        {
            MailMessage email = new MailMessage();
            MailAddress from = new MailAddress(_configFileHelper.EmailFromAddress);
            email.From = from;
            MailAddress to = new MailAddress(_configFileHelper.EmailToAddress);
            email.To.Add(to);
            MailAddress cc = new MailAddress(_configFileHelper.EmailCCAddresses);
            email.CC.Add(cc); // may have to review this......
            MailAddress replyTo = new MailAddress(_configFileHelper.EmailReplyToAddress);
            email.ReplyTo = replyTo;    // Consider using replyToList....
            email.Subject = _configFileHelper.EmailSubject;

            if ((prsUnavailableErrorLimitReached == false) && (prsTotalErrorLimitReached == false))
            {
                // Total limit has been reached
                email.Body = _configFileHelper.TotalErrorLimitExceededBodyText;
                return email;
            }

            string _bodyText = null;

            if (prsUnavailableErrorLimitReached == true)
            {
                _bodyText += _configFileHelper.UnavailableErrorLimitExceededBodyText;
            }
            if (prsTotalErrorLimitReached == true)
            {
                _bodyText += _configFileHelper.TimeoutErrorLimitExceededBodyText;
            }

            email.Body = _bodyText;

            return email;
        }
    }
}
