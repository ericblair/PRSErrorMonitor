using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRSErrorMonitor;

namespace PRSErrorMonitorTests.TestHelpers.PopulateDatabaseTables
{
    public static class AddRowTotbPrsErrorMonitor
    {
        public static tbRPT_PRSErrorMonitor AddTableRow()
        {
            tbRPT_PRSErrorMonitor row = new tbRPT_PRSErrorMonitor
            {
                DateTime = DateTime.Now,
                PRSUnavailableErrors = 0,
                PRSTimeoutErrors = 0
            };

            return row;
        }

        public static tbRPT_PRSErrorMonitor AddTableRow(DateTime dateTime)
        {
            tbRPT_PRSErrorMonitor row = AddTableRow();
            row.DateTime = dateTime;

            return row;
        }

        public static tbRPT_PRSErrorMonitor AddTableRow(DateTime dateTime, int numPrsUnavailableErrors, int numPrsTimeoutErrors)
        {
            tbRPT_PRSErrorMonitor row = new tbRPT_PRSErrorMonitor
            {
                DateTime = dateTime,
                PRSUnavailableErrors = numPrsUnavailableErrors,
                PRSTimeoutErrors = numPrsTimeoutErrors
            };

            return row;
        }
    }
}
