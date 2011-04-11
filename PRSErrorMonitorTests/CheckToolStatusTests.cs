using PRSErrorMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace PRSErrorMonitorTests
{
    /// <summary>
    ///This is a test class for CheckPrsErrorActivtyTest and is intended
    ///to contain all CheckPrsErrorActivtyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CheckToolStatusTests
    {
        IConfigFileHelper _configFileHelperStub;
        Mock<ICheckPrsErrorLevels> _checkPrsErrorLevels;
        ILogger _log;

        [TestInitialize]
        public void TestInitialize()
        {
            _configFileHelperStub = new TestHelpers.Stubs.ConfigFileHelperStub();
            _checkPrsErrorLevels = new Mock<ICheckPrsErrorLevels>();
            _log = new Logger();
        }
        
        [TestMethod]
        public void CheckErrorFrequencyEqualsOneMinute_CallsCheckIfPrsHasExceededErrorLimit()
        {
            _configFileHelperStub.PrsErrorCheckFrequency = 1;

            CheckToolStatus _checkToolStatus = new CheckToolStatus(_configFileHelperStub, _checkPrsErrorLevels.Object, _log);
            _checkToolStatus.DetermineIfPrsErrorActivityShouldBeChecked();

            _checkPrsErrorLevels.Verify(x => x.CheckIfPrsHasExceededErrorLimit(), Times.Once());
        }

        [TestMethod]
        public void LastCheckWasPerformedLongEnoughAgoToNeedRepeated_CheckIfPrsHasExceededErrorLimit_Called()
        {
            _configFileHelperStub.PrsErrorCheckFrequency = 10;
            _configFileHelperStub.TimeLastPrsErrorCheckWasRun = DateTime.Now.AddMinutes(-11);

            CheckToolStatus _checkToolStatus = new CheckToolStatus(_configFileHelperStub, _checkPrsErrorLevels.Object, _log);
            _checkToolStatus.DetermineIfPrsErrorActivityShouldBeChecked();

            _checkPrsErrorLevels.Verify(x => x.CheckIfPrsHasExceededErrorLimit(), Times.Once());
        }

        [TestMethod]
        public void LastCheckWasPerformedTooRecentlyToBeRepeated_CheckIfPrsHasExceededErrorLimit_NotCalled()
        {
            _configFileHelperStub.PrsErrorCheckFrequency = 10;
            _configFileHelperStub.TimeLastPrsErrorCheckWasRun = DateTime.Now.AddMinutes(-9);

            CheckToolStatus _checkToolStatus = new CheckToolStatus(_configFileHelperStub, _checkPrsErrorLevels.Object, _log);
            _checkToolStatus.DetermineIfPrsErrorActivityShouldBeChecked();

            _checkPrsErrorLevels.Verify(x => x.CheckIfPrsHasExceededErrorLimit(), Times.Never());
        }
    }
}
