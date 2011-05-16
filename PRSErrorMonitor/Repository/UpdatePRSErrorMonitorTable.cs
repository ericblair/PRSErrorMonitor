using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    public partial class Repository
    {
        public void UpdatePRSErrorMonitorTable(DateTime dateTime, int prsUnavailableErrors, int prsTimeoutErrors)
        {
            tbRPT_PRSErrorMonitor _prsErrorMonitorRow = new tbRPT_PRSErrorMonitor();
            _prsErrorMonitorRow.DateTime = dateTime;
            _prsErrorMonitorRow.PRSUnavailableErrors = prsUnavailableErrors;
            _prsErrorMonitorRow.PRSTimeoutErrors = prsTimeoutErrors;

            _reportingEntityContext.tbRPT_PRSErrorMonitor.AddObject(_prsErrorMonitorRow);
            _reportingEntityContext.SaveChanges();
        }
    }
}
