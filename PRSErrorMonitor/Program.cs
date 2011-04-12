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

                // NotifyPartiesOfPrsIssues: used to send an email warning of PRS errors if the limits have been exceeded
                // (which is checked in CheckPrsErrorLevels, which itself is called in DetermineIfPrsErrorActivityShouldBeChecked)
                // This is a poor design as two objects are instantiated despite possibly not being required

                DetermineIfPrsErrorActivityShouldBeChecked _checkToolStatus = new DetermineIfPrsErrorActivityShouldBeChecked(_configFileHelper, _log);
                bool _shouldPrsErrorCheckBeRun = _checkToolStatus.Run();

                // call CheckPrsErrorLevels like i called DetermineIfPrsErrorActivityShouldBeChecked above and then NotifyPartiesOfPrsIssues after

                NotifyPartiesOfPrsIssues _notifyPartiesOfPrsIssues = new NotifyPartiesOfPrsIssues(_log);
                //CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_repository, _configFileHelper, _notifyPartiesOfPrsIssues, _log);
                
                
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
