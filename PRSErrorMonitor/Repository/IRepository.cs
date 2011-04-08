using System;
using System.Collections.Generic;

namespace PRSErrorMonitor
{
    public interface IRepository
    {
        global::PRSErrorMonitor.tbPRSErrorMonitor RetrieveLatestRowFromPrsErrorMonitorTable();
        int ReturnErrorCountInPreviousMinute(DateTime timeToCheck, int? errorCode);
        void UpdatePRSErrorMonitorTable(DateTime dateTime, int prsUnavailableErrors, int prsTimeoutErrors);
        IList<tbPRSErrorMonitor> RetrieveRowsFromPrsErrorMonitorTableForTimeSpecified(int checkFrequencyMinutes);
    }
}
