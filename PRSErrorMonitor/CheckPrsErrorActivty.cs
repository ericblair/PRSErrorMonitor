using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PRSErrorMonitor
{
    static class CheckPrsErrorActivty
    {
        public static void DetermineIfPrsErrorActivityShouldBeChecked()
        {
            int _errorCheckFrequency = Convert.ToInt32(ConfigurationManager.AppSettings["PrsErrorCheckFrequency"]);
            // check errorCheckFrequency != 0

            DateTime _lastTimePrsErrorsWereChecked = Convert.ToDateTime(ConfigurationManager.AppSettings["TimeLastPrsErrorCheckWasRun"]);
            // check for conversion errors

            if (_errorCheckFrequency == 1)
            {
                // Run check
            }
            else if ((_lastTimePrsErrorsWereChecked.AddSeconds(_errorCheckFrequency * 60) <= DateTime.Now))
            {
                // Run Check
            }
            else
            {
                // don't run check
            }

        }

        public static void CheckPrsErrorActivity(Repository repository)
        {
            int _prsUnavailableErrorLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PrsUnavailableErrorLimit"]); // check for conversion error / zero value
            int _prsTimeoutErrorLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PrsTimeoutErrorLimit"]); // as above
            int _prsTotalErrorLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PrsTotalErrorLimit"]); // as above

            // think just grabbing the last row from the database should be safe enough but i may have to check the dateTime colum to ensure 
            // it was created within a close enough range
            tbPRSErrorMonitor _resultsFromLatestPrsCheck = repository.RetrieveLatestRowFromPrsErrorMonitorTable();

            bool _limitHasBeenExceeded = false;
            bool _prsUnavailableErrorLimitExceeded = false;
            bool _prsTimeoutErrorLimitExceeded = false;
            bool _prsTotalErrorLimitExceeded = false;

            if (_resultsFromLatestPrsCheck.PRSUnavailableErrors >= _prsUnavailableErrorLimit)
            {
                _prsUnavailableErrorLimitExceeded = true;
                _limitHasBeenExceeded = true;
            }

            if (_resultsFromLatestPrsCheck.PRSTimeoutErrors >= _prsTimeoutErrorLimit)
            {
                _prsTimeoutErrorLimitExceeded = true;
                _limitHasBeenExceeded = true;
            }

            if (_resultsFromLatestPrsCheck.PRSUnavailableErrors + _resultsFromLatestPrsCheck.PRSTimeoutErrors >= _prsTotalErrorLimit)
            {
                _prsTotalErrorLimitExceeded = true;
                _limitHasBeenExceeded = true;
            }

            if (_limitHasBeenExceeded == true)
            {
                NotifyPartiesOfPrsIssues.SendEmailToHelpdesk(_prsTimeoutErrorLimitExceeded, _prsUnavailableErrorLimitExceeded, _prsTotalErrorLimitExceeded);
            }
        }
    }
}
