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
            // Reason for me using ToList<> : http://www.ginktage.com/2010/10/not-supported-in-linq-to-entities-and-entity-framework/
            var _lastRecord = (from reporting in _reportingEntityContext.tbPRSErrorMonitor
                               select reporting).ToList<tbPRSErrorMonitor>().LastOrDefault();
                                //_reportingEntityContext.tbPRSErrorMonitor.OrderBy(reporting => reporting.DateTime).Last();
                                //(from reporting in _reportingEntityContext.tbPRSErrorMonitor
                                // select reporting).LastOrDefault();

            return _lastRecord;
        }
    }
}
