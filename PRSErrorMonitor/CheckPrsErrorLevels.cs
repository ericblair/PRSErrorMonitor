using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    public class CheckPrsErrorLevels : PRSErrorMonitor.ICheckPrsErrorLevels
    {
        IRepository _repository;
        IConfigFileHelper _configFileHelper;
        INotifyPartiesOfPrsIssues _notifyPartiesOfPrsIssues;

        public CheckPrsErrorLevels(IRepository repository, IConfigFileHelper configFileHelper, INotifyPartiesOfPrsIssues notifyPartiesOfPrsIssues)
        {
            _repository = repository;
            _configFileHelper = configFileHelper;
            _notifyPartiesOfPrsIssues = notifyPartiesOfPrsIssues;
        }

        public void CheckIfPrsHasExceededErrorLimit()
        {
            // First check that an issue hasn't already been reported
            if (_configFileHelper.EmailSentFlag == 1) return;

            int _prsUnavailableErrorLimit = _configFileHelper.PrsUnavailableErrorLimit; // check for conversion error / zero value
            int _prsTimeoutErrorLimit = _configFileHelper.PrsTimeoutErrorLimit; // as above
            int _prsTotalErrorLimit = _configFileHelper.PrsTotalErrorLimit; // as above

            int _numberOfPrsUnavailableErrors = 0;
            int _numberOfPrsTimeoutErrors = 0;
            int _totalNumberOfPrsErrors = 0;

            if (_configFileHelper.PrsErrorCheckFrequency == 1)  // If check is to be run every minute, send email without worring about how long error has occured for
            {
                tbPRSErrorMonitor _resultsFromLatestPrsCheck = _repository.RetrieveLatestRowFromPrsErrorMonitorTable();
                _numberOfPrsUnavailableErrors = _resultsFromLatestPrsCheck.PRSUnavailableErrors;
                _numberOfPrsTimeoutErrors = _resultsFromLatestPrsCheck.PRSTimeoutErrors;
                _totalNumberOfPrsErrors = _numberOfPrsUnavailableErrors + _numberOfPrsTimeoutErrors;
            }
            else
            {
                IList<tbPRSErrorMonitor> _prsErrorDetailsSinceLastCheck = 
                    _repository.RetrieveRowsFromPrsErrorMonitorTableForTimeSpecified(_configFileHelper.PrsErrorCheckFrequency);

                foreach (var prsMonitorRow in _prsErrorDetailsSinceLastCheck)
                {
                    _numberOfPrsUnavailableErrors += prsMonitorRow.PRSUnavailableErrors;
                    _numberOfPrsTimeoutErrors += prsMonitorRow.PRSTimeoutErrors;
                }

                _totalNumberOfPrsErrors = _numberOfPrsUnavailableErrors + _numberOfPrsTimeoutErrors;
            }

            bool _limitHasBeenExceeded = false;
            bool _prsUnavailableErrorLimitExceeded = false;
            bool _prsTimeoutErrorLimitExceeded = false;
            bool _prsTotalErrorLimitExceeded = false;

            if (_numberOfPrsUnavailableErrors >= _prsUnavailableErrorLimit)
            {
                _prsUnavailableErrorLimitExceeded = true;
                _limitHasBeenExceeded = true;
            }

            if (_numberOfPrsTimeoutErrors >= _prsTimeoutErrorLimit)
            {
                _prsTimeoutErrorLimitExceeded = true;
                _limitHasBeenExceeded = true;
            }

            if (_totalNumberOfPrsErrors >= _prsTotalErrorLimit)
            {
                _prsTotalErrorLimitExceeded = true;
                _limitHasBeenExceeded = true;
            }

            if (_limitHasBeenExceeded == true)
            {
                // Check that notification email has not already been sent out
                if (_configFileHelper.EmailSentFlag == 1) return;
                _notifyPartiesOfPrsIssues.SendEmailToHelpdesk(_prsUnavailableErrorLimitExceeded, _prsTimeoutErrorLimitExceeded, _prsTotalErrorLimitExceeded);
            }

            // Update last check run time
            _configFileHelper.TimeLastPrsErrorCheckWasRun = DateTime.Now;
        }
    }
}
