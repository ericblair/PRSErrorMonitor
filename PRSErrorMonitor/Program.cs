using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger _log = new Logger();
            ePharmacyEntities _ePharmEntity = new ePharmacyEntities();
            ReportingEntities _reportingEntity = new ReportingEntities();
            Repository _repository = new Repository(_ePharmEntity, _reportingEntity);

            try
            {
                // Check ePharmacy.tbAuditExchangeInbound for any PRS errors that have occured in the previous minute
                RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_repository, _log);
                _recordPrsErrorActivity.RecordPRSErrorCounts();

                // ConfigFileHelper: Utility class to assist reading from / writing to the config file
                ConfigFileHelper _configFileHelper = new ConfigFileHelper();

                DetermineIfPrsErrorActivityShouldBeChecked _checkToolStatus = new DetermineIfPrsErrorActivityShouldBeChecked(_configFileHelper, _log);
                bool _shouldPrsErrorCheckBeRun = _checkToolStatus.Run();

                CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_repository, _configFileHelper, _log);
                bool _unavailableErrorLimitExceeded = _checkPrsErrorLevels.PrsUnavailableErrorLevelExceeded();
                bool _timeoutErrorLimitExceeded = _checkPrsErrorLevels.PrsTimeoutErrorLevelExceeded();
                bool _totalErrorLimitExceeded = _checkPrsErrorLevels.PrsTotalErrorLevelExceeded();

                // If either of the bool values are true, then send a warning email
                if (_unavailableErrorLimitExceeded || _timeoutErrorLimitExceeded || _totalErrorLimitExceeded)
                {
                    ReportPrsErrors _reportErrors = new ReportPrsErrors(_log);
                    _reportErrors.SendEmail(_unavailableErrorLimitExceeded, _timeoutErrorLimitExceeded, _totalErrorLimitExceeded);
                }
            }
            catch(Exception ex)
            {
                _log.Add("Error Detected: " + ex.Message + ex.InnerException);
            }
            finally
            {
                _log.Write();
            }
        }
    }
}
