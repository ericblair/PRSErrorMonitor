using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    /// <summary>
    /// This class contains methods that determine whether the number of PRS errors exceed the limits specified in the config file
    /// </summary>
    public class CheckPrsErrorLevels : PRSErrorMonitor.ICheckPrsErrorLevels
    {
        IRepository _repository;
        IConfigFileHelper _configFileHelper;
        ILogger _log;

        int _prsUnavailableErrorLimit;
        int _prsTimeoutErrorLimit;
        int _prsTotalErrorLimit;

        int _numberOfPrsUnavailableErrors = 0;
        int _numberOfPrsTimeoutErrors = 0;
        int _totalNumberOfPrsErrors = 0;

        public CheckPrsErrorLevels(IRepository repository, IConfigFileHelper configFileHelper, ILogger log)
        {
            _repository = repository;
            _configFileHelper = configFileHelper;
            _log = log;

            _prsUnavailableErrorLimit = _configFileHelper.PrsUnavailableErrorLimit;
            _prsTimeoutErrorLimit = _configFileHelper.PrsTimeoutErrorLimit;
            _prsTotalErrorLimit = _configFileHelper.PrsTotalErrorLimit;

            // If check is to be run every minute, just grab the latest row from tbPrsErrorMonitor
            if (_configFileHelper.PrsErrorCheckFrequency == 1)
            {
                tbPRSErrorMonitor _resultsFromLatestPrsCheck = _repository.RetrieveLatestRowFromPrsErrorMonitorTable();
                _numberOfPrsUnavailableErrors = _resultsFromLatestPrsCheck.PRSUnavailableErrors;
                _numberOfPrsTimeoutErrors = _resultsFromLatestPrsCheck.PRSTimeoutErrors;
                _totalNumberOfPrsErrors = _numberOfPrsUnavailableErrors + _numberOfPrsTimeoutErrors;
            }
            else  // Grab all the rows created since check was last ran
            {
                // Been thinking about changing this to retrieive all records more recent than the lastTimeCheckWasRan 
                // value in the config file as its more accurate and probably the same speed.....?
                IList<tbPRSErrorMonitor> _prsErrorDetailsSinceLastCheck =
                    _repository.RetrieveRowsFromPrsErrorMonitorTableForTimeSpecified(_configFileHelper.PrsErrorCheckFrequency);

                foreach (var tableRow in _prsErrorDetailsSinceLastCheck)
                {
                    _numberOfPrsUnavailableErrors += tableRow.PRSUnavailableErrors;
                    _numberOfPrsTimeoutErrors += tableRow.PRSTimeoutErrors;
                }

                _totalNumberOfPrsErrors = _numberOfPrsUnavailableErrors + _numberOfPrsTimeoutErrors;
            }

            // Update the config file with the time check was run
            _configFileHelper.UpdateTimeLastPrsErrorCheckWasRun();
        }

        public bool PrsIssueHasAlreadyBeenReported()
        {
            // Read the flag value from the config file (should only ever be zero or one)
            int _emailAlreadySentFlag = _configFileHelper.EmailSentFlag;

            if (_emailAlreadySentFlag == 1)
                return true;

            return false;
        }

        public bool PrsUnavailableErrorLevelExceeded()
        {
            if (_numberOfPrsUnavailableErrors >= _prsUnavailableErrorLimit)
            {
                return true;
            }
            return false;
        }

        public bool PrsTimeoutErrorLevelExceeded()
        {
            if (_numberOfPrsTimeoutErrors >= _prsTimeoutErrorLimit)
            {
                return true;
            }
            return false;
        }

        public bool PrsTotalErrorLevelExceeded()
        {
            if (_totalNumberOfPrsErrors >= _prsTotalErrorLimit)
            {
                return true;
            }
            return false;
        }
    }
}
