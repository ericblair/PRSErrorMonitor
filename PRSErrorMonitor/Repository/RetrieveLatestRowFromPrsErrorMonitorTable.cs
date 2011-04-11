using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    public partial class Repository
    {
        public tbPRSErrorMonitor RetrieveLatestRowFromPrsErrorMonitorTable()
        {
            var _lastRecord = (from reporting in _reportingEntityContext.tbPRSErrorMonitor
                               select reporting).ToList<tbPRSErrorMonitor>().LastOrDefault();
                                //_reportingEntityContext.tbPRSErrorMonitor.OrderBy(reporting => reporting.DateTime).Last();
                                //(from reporting in _reportingEntityContext.tbPRSErrorMonitor
                                // select reporting).LastOrDefault();

            return _lastRecord;
        }
    }
}
