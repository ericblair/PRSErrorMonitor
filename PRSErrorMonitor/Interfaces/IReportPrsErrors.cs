using System;
namespace PRSErrorMonitor
{
    public interface IReportPrsErrors
    {
        void SendEmail(bool prsUnavailableErrorLimitReached, bool prsTimeoutErrorLimitReached, bool prsTotalErrorLimitReached);
    }
}
