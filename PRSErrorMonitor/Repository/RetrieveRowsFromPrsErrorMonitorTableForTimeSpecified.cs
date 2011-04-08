using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    public partial class Repository
    {
        public IList<tbPRSErrorMonitor> RetrieveRowsFromPrsErrorMonitorTableForTimeSpecified(int checkFrequencyMinutes)
        {
            DateTime _beginTime = DateTime.Now.AddMinutes(-checkFrequencyMinutes);

            var _recordsWithinSpecifiedTimePeriod = (from reporting in _reportingEntityContext.tbPRSErrorMonitor
                                                     where reporting.DateTime > _beginTime
                                                     select reporting).ToList();

            return _recordsWithinSpecifiedTimePeriod;
        }
    }
}
