using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PRSErrorMonitor
{
    /// <summary>
    /// This class reads how often PRS errors are to be checked from the config file and returns true if enough time has 
    /// elapsed for the number of errors to be checked again.
    /// </summary>
    public class DetermineIfPrsErrorActivityShouldBeChecked
    {
        IConfigFileHelper _configFileHelper;
        ILogger _log;

        public DetermineIfPrsErrorActivityShouldBeChecked(IConfigFileHelper configFileHelper, ILogger log)
        {
            _configFileHelper = configFileHelper;
            _log = log;
        }

        public bool Run()
        {
            try
            {
                // Check to see if helpdesk has already been notified of PRS issues without the emailSent flag having been reset
                if (_configFileHelper.EmailSentFlag == 1)
                {
                    // No more warning emails should be sent until Support team sets EmailSent flag to zero
                    return false;
                }

                // Read how often check should be ran (in minutes)
                int _errorCheckFrequency = _configFileHelper.PrsErrorCheckFrequency;
                
                // This tool will be run from a scheduled task once a minute.
                if (_errorCheckFrequency == 1)
                {
                    // _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();
                    return true;
                }

                // Grab the time of the last run from the config file
                DateTime _lastTimePrsErrorsWereChecked = _configFileHelper.TimeLastPrsErrorCheckWasRun;
                
                if ((_lastTimePrsErrorsWereChecked.AddSeconds(_errorCheckFrequency * 60) <= DateTime.Now))
                {
                    //_checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();
                    return true;
                }
                
                // Check is not to be run
                return false;
            }
            catch(Exception ex)
            {
                _log.Add("Error Detected: " + ex.Message + ex.InnerException);
                // throw error
                return false;
            }
        }

        
    }
}
