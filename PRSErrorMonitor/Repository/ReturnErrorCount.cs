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
            int secondsToDeduct = timeToCheck.Second;
            DateTime _timeToCheckLower = timeToCheck.AddSeconds(-secondsToDeduct);
            DateTime _timeToCheckUpper = _timeToCheckLower.AddSeconds(59);


            var temp = (from AEI in _ePharmacyEntityContext.tbAuditExchangeInbound
                              where AEI.diagNumber == errorCode
                              && AEI.auditCreatedOn >= _timeToCheckLower
                              && AEI.auditCreatedOn <= _timeToCheckUpper
                              select AEI);

            int errorCount = temp.Count();

            return errorCount;
        }
    }
}
