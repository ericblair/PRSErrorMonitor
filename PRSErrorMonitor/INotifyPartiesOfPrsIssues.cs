using System;
namespace PRSErrorMonitor
{
    public interface INotifyPartiesOfPrsIssues
    {
        void SendEmailToHelpdesk(bool prsUnavailableErrorLimitReached, bool prsTimeoutErrorLimitReached, bool prsTotalErrorLimitReached);
    }
}
