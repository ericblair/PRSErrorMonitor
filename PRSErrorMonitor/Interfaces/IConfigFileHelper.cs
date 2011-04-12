using System;

namespace PRSErrorMonitor
{
    public interface IConfigFileHelper
    {
        string EmailCCAddresses { get; set; }
        string EmailFromAddress { get; set; }
        string EmailReplyToAddress { get; set; }
        string EmailSubject { get; set; }
        string EmailToAddress { get; set; }
        int PrsErrorCheckFrequency { get; set; }
        int PrsTimeoutErrorLimit { get; set; }
        int PrsTotalErrorLimit { get; set; }
        int PrsUnavailableErrorLimit { get; set; }
        DateTime TimeLastPrsErrorCheckWasRun { get; set; }
        string TimeoutErrorLimitExceededBodyText { get; set; }
        string TotalErrorLimitExceededBodyText { get; set; }
        string UnavailableErrorLimitExceededBodyText { get; set; }
        int EmailSentFlag { get; set; }

        void UpdateTimeLastPrsErrorCheckWasRun();
        void RecordEmailHavingBeenSent();
    }
}
