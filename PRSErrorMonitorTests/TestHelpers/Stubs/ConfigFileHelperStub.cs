using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRSErrorMonitor;

namespace PRSErrorMonitorTests.TestHelpers.Stubs
{
    public class ConfigFileHelperStub : IConfigFileHelper
    {
        public string EmailCCAddresses { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailReplyToAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailToAddress { get; set; }
        public int PrsErrorCheckFrequency { get; set; }
        public int PrsTimeoutErrorLimit { get; set; }
        public int PrsTotalErrorLimit { get; set; }
        public int PrsUnavailableErrorLimit { get; set; }
        public DateTime TimeLastPrsErrorCheckWasRun { get; set; }
        public string TimeoutErrorLimitExceededBodyText { get; set; }
        public string TotalErrorLimitExceededBodyText { get; set; }
        public string UnavailableErrorLimitExceededBodyText { get; set; }
        public int EmailSentFlag { get; set; }

        public void RecordEmailHavingBeenSent() 
        {
            EmailSentFlag = 1;
        }
        public void UpdateTimeLastPrsErrorCheckWasRun() 
        {
            TimeLastPrsErrorCheckWasRun = DateTime.Now;
        }
    }
}
