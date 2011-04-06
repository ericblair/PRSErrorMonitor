using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRSErrorMonitor;

namespace PRSErrorMonitorTests.TestHelpers.PopulateDatabaseTables
{
    public static class AddRowTotbPrsErrorMonitor
    {
        public static tbPRSErrorMonitor AddTableRow()
        {
            tbPRSErrorMonitor row = new tbPRSErrorMonitor
            {
                DateTime = DateTime.Now,
                PRSUnavailableErrors = 0,
                PRSTimeoutErrors = 0
            };

            return row;
        }

        public static tbPRSErrorMonitor AddTableRow(DateTime dateTime)
        {
            tbPRSErrorMonitor row = AddTableRow();
            row.DateTime = dateTime;

            return row;
        }

        public static tbPRSErrorMonitor AddTableRow(DateTime dateTime, int numPrsUnavailableErrors, int numPrsTimeoutErrors)
        {
            tbPRSErrorMonitor row = new tbPRSErrorMonitor
            {
                DateTime = dateTime,
                PRSUnavailableErrors = numPrsUnavailableErrors,
                PRSTimeoutErrors = numPrsTimeoutErrors
            };

            return row;
        }
    }
}
