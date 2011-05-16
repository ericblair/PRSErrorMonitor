using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    public partial class Repository
    {
        public IList<tbRPT_PRSErrorMonitor> RetrieveRowsFromPrsErrorMonitorTableForTimeSpecified(int checkFrequencyMinutes)
        {
            DateTime _beginTime = DateTime.Now.AddMinutes(-checkFrequencyMinutes);

            var _recordsWithinSpecifiedTimePeriod = (from reporting in _reportingEntityContext.tbRPT_PRSErrorMonitor
                                                     where reporting.DateTime > _beginTime
                                                     select reporting).ToList();

            return _recordsWithinSpecifiedTimePeriod;
        }
    }
}
