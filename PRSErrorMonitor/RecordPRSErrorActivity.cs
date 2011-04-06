﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    static class RecordPRSErrorActivity
    {
        /// <summary>
        /// This method should be called from a scheduled task once a minute and will check the error counts for the previous minute.
        /// Upon completion, this method should add a new row to tbPRSErrorMonitor with the number of errors to have occured in the previous minute.
        /// </summary>
        public static void RecordPRSErrorCounts(Repository repository)
        {
            int _numberOfPRSUnavailableErrors = 0;
            int _numberOfPRSTimeoutErrors = 0;

            int _prsUnavailableErrorCode = 3000;
            int _prsTimeoutErrorCode = 3005;

            DateTime _previousMinute = DateTime.Now.AddMinutes(-1);

            // try / catch

            // Find out how many PRS errors occured in the previous minute
            _numberOfPRSUnavailableErrors = repository.ReturnErrorCountInPreviousMinute(_previousMinute, _prsUnavailableErrorCode);
            _numberOfPRSTimeoutErrors = repository.ReturnErrorCountInPreviousMinute(_previousMinute, _prsTimeoutErrorCode);

            // Save details of PRS errors to tbPRSErrorMonitor
            repository.UpdatePRSErrorMonitorTable(_previousMinute, _prsUnavailableErrorCode, _prsTimeoutErrorCode);
        }
    }
}