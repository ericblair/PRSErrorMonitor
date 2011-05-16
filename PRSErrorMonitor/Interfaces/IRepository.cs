using System;
using System.Collections.Generic;

namespace PRSErrorMonitor
{
    public interface IRepository
    {
        global::PRSErrorMonitor.tbRPT_PRSErrorMonitor RetrieveLatestRowFromPrsErrorMonitorTable();
        int ReturnErrorCountInPreviousMinute(DateTime timeToCheck, int? errorCode);
        void UpdatePRSErrorMonitorTable(DateTime dateTime, int prsUnavailableErrors, int prsTimeoutErrors);
        IList<tbRPT_PRSErrorMonitor> RetrieveRowsFromPrsErrorMonitorTableForTimeSpecified(int checkFrequencyMinutes);
    }
}
