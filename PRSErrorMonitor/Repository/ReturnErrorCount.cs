using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    public partial class Repository
    {
        /// <summary>
        /// This method will return the number of times the error matching the parameter passed to it has occured in the previous minute
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public int ReturnErrorCountInPreviousMinute(DateTime timeToCheck, int? errorCode)
        {
            DateTime _timeToCheck = timeToCheck;
            int secondsToDeduct = _timeToCheck.Second;
            _timeToCheck = _timeToCheck.AddSeconds(-secondsToDeduct);

            var temp = (from AEI in _ePharmacyEntityContext.tbAuditExchangeInbound
                              where AEI.diagNumber == errorCode
                              && AEI.auditCreatedOn >= _timeToCheck
                              && AEI.auditCreatedOn <= _timeToCheck.AddSeconds(59)
                              select AEI);

            int errorCount = temp.Count();

            return errorCount;
        }
    }
}
