using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    /// <summary>
    /// Checks the live database for any PRS issues and records them in the Reporting.tbPrsErrorMonitor table
    /// </summary>
    class RecordPRSErrorActivity
    {
        private IRepository _repository;
        private ILogger _log;
        
        // Standard usage and unit testing constructor
        public RecordPRSErrorActivity(IRepository repository, ILogger log)
        {
            _repository = repository;
            _log = log;
        }

        /// <summary>
        /// This method should be called from a scheduled task once a minute and will check the error counts for the previous minute.
        /// Upon completion, this method should add a new row to tbPRSErrorMonitor with the number of errors to have occured in the previous minute.
        /// </summary>
        public void RecordPRSErrorCounts()
        {
            int _numberOfPRSUnavailableErrors = 0;
            int _numberOfPRSTimeoutErrors = 0;

            int _prsUnavailableErrorCode = 3000;
            int _prsTimeoutErrorCode = 3005;

            DateTime _previousMinute = DateTime.Now.AddMinutes(-1);

            try
            {
                // Find out how many PRS errors occured in the previous minute
                _numberOfPRSUnavailableErrors = _repository.ReturnErrorCountInPreviousMinute(_previousMinute, _prsUnavailableErrorCode);
                _numberOfPRSTimeoutErrors = _repository.ReturnErrorCountInPreviousMinute(_previousMinute, _prsTimeoutErrorCode);

                // Save details of PRS errors to tbPRSErrorMonitor
                _repository.UpdatePRSErrorMonitorTable(_previousMinute, _numberOfPRSUnavailableErrors, _numberOfPRSTimeoutErrors);
            }
            catch(Exception ex)
            {
                _log.Add("Error Detected: " + ex.Message + ex.InnerException);
            }
        }
    }
}
