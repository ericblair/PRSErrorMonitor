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
            ePharmacyEntities _ePharmEntity = new ePharmacyEntities();
            ReportingEntities _reportingEntity = new ReportingEntities();
            Repository _repository = new Repository(_ePharmEntity, _reportingEntity);

            RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_repository);
            _recordPrsErrorActivity.RecordPRSErrorCounts();

            ConfigFileHelper _configFileHelper = new ConfigFileHelper();
            NotifyPartiesOfPrsIssues _notifyPartiesOfPrsIssues = new NotifyPartiesOfPrsIssues();
            CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_repository, _configFileHelper, _notifyPartiesOfPrsIssues);
            CheckToolStatus _checkToolStatus = new CheckToolStatus(_configFileHelper, _checkPrsErrorLevels);
        }
    }
}
