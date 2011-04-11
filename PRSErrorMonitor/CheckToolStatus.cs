using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PRSErrorMonitor
{
    public class CheckToolStatus
    {
        IConfigFileHelper _configFileHelper;
        ICheckPrsErrorLevels _checkPrsErrorLevels;
        ILogger _log;

        public CheckToolStatus(IConfigFileHelper configFileHelper, ICheckPrsErrorLevels checkPrsErrorLevels, ILogger log)
        {
            _configFileHelper = configFileHelper;
            _checkPrsErrorLevels = checkPrsErrorLevels;
            _log = log;
        }

        public void DetermineIfPrsErrorActivityShouldBeChecked()
        {
            try
            {
                int _errorCheckFrequency = _configFileHelper.PrsErrorCheckFrequency;
                // check errorCheckFrequency != 0

                DateTime _lastTimePrsErrorsWereChecked = _configFileHelper.TimeLastPrsErrorCheckWasRun;
                // check for conversion errors

                if (_errorCheckFrequency == 1)
                {
                    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();
                }
                else if ((_lastTimePrsErrorsWereChecked.AddSeconds(_errorCheckFrequency * 60) <= DateTime.Now))
                {
                    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                _log.Add("Error Detected: " + ex.Message + ex.InnerException);
            }
        }

        
    }
}
