using PRSErrorMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace PRSErrorMonitorTests
{
    /// <summary>
    ///This is a test class for CheckPrsErrorLevelsTest and is intended
    ///to contain all CheckPrsErrorLevelsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CheckPrsErrorLevelsTest
    {
        IePharmacyEntities _ePharmMockEntity;
        IReportingEntities _reportingMockEntity;
        IRepository _mockRepository;
        IConfigFileHelper _configFileHelperStub;
        Mock<INotifyPartiesOfPrsIssues> _notifyPartiesOfPrsIssues;
        ILogger _log;
        CheckPrsErrorLevels _checkPrsErrorLevels;

        [TestInitialize]
        public void TestInitialize()
        {
            _ePharmMockEntity = new ePharmacyEntitiesMock();
            _reportingMockEntity = new ReportingEntitiesMock();
            _mockRepository = new Repository(_ePharmMockEntity, _reportingMockEntity);
            _configFileHelperStub = new TestHelpers.Stubs.ConfigFileHelperStub();
            _log = new Logger();
            _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _log);
        }

        #region OldTestMethods
        
        //[TestMethod]
        //public void CheckErrorsEachMinute_OnlyUnavailableErrorsExist_ErrorLimitNotExceeded_EmailNotSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 5;
        //    int _numOfTimeoutErrors = 0;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorsEachMinute_OnlyUnavailableErrorsExist_ErrorLimitExceeded_EmailSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 20;
        //    int _numOfTimeoutErrors = 0;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorsEachMinute_OnlyTimeoutErrorsExist_ErrorLimitNotExceeded_EmailNotSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 0;
        //    int _numOfTimeoutErrors = 5;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorsEachMinute_OnlyTimeoutErrorsExist_ErrorLimitExceeded_EmailSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 0;
        //    int _numOfTimeoutErrors = 20;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorsEachMinute_UnavailableAndTimeoutErrorsExist_TotalErrorLimitNotExceeded_EmailNotSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 10;
        //    int _numOfTimeoutErrors = 10;
        //    int _unavailableErrorLimit = 50, _timeoutErrorLimit = 50, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorsEachMinute_UnavailableAndTimeoutErrorsExist_ErrorLimitExceeded_EmailSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 30;
        //    int _numOfTimeoutErrors = 30;
        //    int _unavailableErrorLimit = 50, _timeoutErrorLimit = 50, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorFrequencyValueIsGreaterThanEachMinute_OnlyUnavailableErrorsExist_ErrorLimitNotExceeded_EmailNotSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 10;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 5;
        //    int _numOfTimeoutErrors = 0;
        //    int _unavailableErrorLimit = 20, _timeoutErrorLimit = 20, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;
        //    // In total, 15 unavailable errors will be present within last 10 minutes
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-9), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-6), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-3), _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorFrequencyValueIsGreaterThanEachMinute_OnlyUnavailableErrorsExist_ErrorLimitExceeded_EmailSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 10;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 10;
        //    int _numOfTimeoutErrors = 0;
        //    int _unavailableErrorLimit = 20, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    // In total, 30 unavailable errors will be present within the last 10 minutes
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-9), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-6), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-3), _numOfUnavailableErrors, _numOfTimeoutErrors));


        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(), 
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorFrequencyValueIsGreaterThanEachMinute_OnlyTimeoutErrorsExist_ErrorLimitNotExceeded_EmailNotSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 10;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 0;
        //    int _numOfTimeoutErrors = 5;
        //    int _unavailableErrorLimit = 20, _timeoutErrorLimit = 20, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-9), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-6), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-3), _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorFrequencyValueIsGreaterThanEachMinute_OnlyTimeoutErrorsExist_ErrorLimitExceeded_EmailSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 10;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 0;
        //    int _numOfTimeoutErrors = 10;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 20, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-9), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-6), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-3), _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorFrequencyValueIsGreaterThanEachMinute_UnavailableAndTimeoutErrorsExist_TotalErrorLimitNotExceeded_EmailNotSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 10;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 10;
        //    int _numOfTimeoutErrors = 10;
        //    int _unavailableErrorLimit = 50, _timeoutErrorLimit = 50, _totalErrorLimit = 100;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-9), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-6), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-3), _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void CheckErrorFrequencyValueIsGreaterThanEachMinute_UnavailableAndTimeoutErrorsExist_ErrorLimitExceeded_EmailSent()
        //{
        //    _configFileHelperStub.PrsErrorCheckFrequency = 10;
        //    DateTime _errorDateTime = DateTime.Now;
        //    int _numOfUnavailableErrors = 10;
        //    int _numOfTimeoutErrors = 10;
        //    int _unavailableErrorLimit = 50, _timeoutErrorLimit = 50, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-9), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-6), _numOfUnavailableErrors, _numOfTimeoutErrors));
        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime.AddMinutes(-3), _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //    // Check config file is updated
        //    Assert.AreEqual(DateTime.Now.AddSeconds(-DateTime.Now.Second).ToString(),
        //                    _configFileHelperStub.TimeLastPrsErrorCheckWasRun.AddSeconds(-_configFileHelperStub.TimeLastPrsErrorCheckWasRun.Second).ToString());
        //}

        //[TestMethod]
        //public void EmailSentFlagHasNotBeenSet_ErrorLimitsHaveBeenExceeded_EmailIsSent()
        //{
        //    _configFileHelperStub.EmailSentFlag = 0;
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;

        //    DateTime _errorDateTime = DateTime.Now;

        //    int _numOfUnavailableErrors = 10;
        //    int _numOfTimeoutErrors = 10;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        //}

        //[TestMethod]
        //public void EmailSentFlagHasBeenSet_ErrorLimitsHaveBeenExceeded_EmailIsNotSent()
        //{
        //    _configFileHelperStub.EmailSentFlag = 1;
        //    _configFileHelperStub.PrsErrorCheckFrequency = 1;

        //    DateTime _errorDateTime = DateTime.Now;

        //    int _numOfUnavailableErrors = 10;
        //    int _numOfTimeoutErrors = 10;
        //    int _unavailableErrorLimit = 10, _timeoutErrorLimit = 10, _totalErrorLimit = 50;

        //    _configFileHelperStub.PrsUnavailableErrorLimit = _unavailableErrorLimit;
        //    _configFileHelperStub.PrsTimeoutErrorLimit = _timeoutErrorLimit;
        //    _configFileHelperStub.PrsTotalErrorLimit = _totalErrorLimit;

        //    _reportingMockEntity.tbPRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(_errorDateTime, _numOfUnavailableErrors, _numOfTimeoutErrors));

        //    CheckPrsErrorLevels _checkPrsErrorLevels = new CheckPrsErrorLevels(_mockRepository, _configFileHelperStub, _notifyPartiesOfPrsIssues.Object, _log);
        //    _checkPrsErrorLevels.CheckIfPrsHasExceededErrorLimit();

        //    _notifyPartiesOfPrsIssues.Verify(x => x.SendEmailToHelpdesk(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never());
        //}

        #endregion

        [TestMethod]
        public void IssueHasAlreadyBeenReporeted_ConfigValueEqualsOne_ReturnsTrue()
        {
            // Arrange
            _configFileHelperStub.EmailSentFlag = 1;
            bool _issueHasBeenReported = false;

            // Act
            _issueHasBeenReported = _checkPrsErrorLevels.PrsIssueHasAlreadyBeenReported();

            // Assert
            Assert.IsTrue(_issueHasBeenReported);
        }

        [TestMethod]
        public void IssueHasAlreadyBeenReporeted_ConfigValueEqualsZero_ReturnsFalse()
        {
            // Arrange
            _configFileHelperStub.EmailSentFlag = 0;
            bool _issueHasBeenReported = false;

            // Act
            _issueHasBeenReported = _checkPrsErrorLevels.PrsIssueHasAlreadyBeenReported();

            // Assert
            Assert.IsFalse(_issueHasBeenReported);
        }
    }
}
