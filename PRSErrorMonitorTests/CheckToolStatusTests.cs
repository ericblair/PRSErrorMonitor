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
        public void CheckErrorFrequencyEqualsOneMinute_ReturnsTrue()
        {
            // Arrange
            _configFileHelperStub.PrsErrorCheckFrequency = 1;
            DetermineIfPrsErrorActivityShouldBeChecked _checkToolStatus = new DetermineIfPrsErrorActivityShouldBeChecked(_configFileHelperStub, _log);
            bool _shouldCheckBeRun = false;
            // Found I had to set the following values after noticing the method returned true if I set the frequency to 2.
            // Turns out the default value for _lastTimePrsErrorsWereChecked was old enough to cause the method to return true in the second check
            _configFileHelperStub.TimeLastPrsErrorCheckWasRun = DateTime.Now;

            // Act
            _shouldCheckBeRun = _checkToolStatus.Run();
            
            // Assert
            Assert.IsTrue(_shouldCheckBeRun, "Check should be run if check frequency equals one minute.");
        }

        // Created this after spotting a failure in CheckErrorFrequencyEqualsOneMinute_ReturnsTrue() (now fixed)
        [TestMethod]
        public void CheckErrorFrequencyEqualsTwoMinutes_LastTimePrsErrorsCheckedEqualsCurrentDateTime_ReturnsFalse()
        {
            // Arrange
            _configFileHelperStub.PrsErrorCheckFrequency = 2;
            DetermineIfPrsErrorActivityShouldBeChecked _checkToolStatus = new DetermineIfPrsErrorActivityShouldBeChecked(_configFileHelperStub, _log);
            bool _shouldCheckBeRun = true; // Defaulting to the value I'm not expecting
            _configFileHelperStub.TimeLastPrsErrorCheckWasRun = DateTime.Now;

            // Act
            _shouldCheckBeRun = _checkToolStatus.Run();

            // Assert
            Assert.IsFalse(_shouldCheckBeRun);
        }

        [TestMethod]
        public void LastCheckWasPerformedLongEnoughAgoToNeedRepeated_ReturnsTrue()
        {
            // Arrange
            _configFileHelperStub.PrsErrorCheckFrequency = 10;  // Errors should be checked every 10 minutes
            _configFileHelperStub.TimeLastPrsErrorCheckWasRun = DateTime.Now.AddMinutes(-11);  // Last check was run 11 minutes ago
            DetermineIfPrsErrorActivityShouldBeChecked _checkToolStatus = new DetermineIfPrsErrorActivityShouldBeChecked(_configFileHelperStub, _log);
            bool _shouldCheckBeRun = false;

            // Act
            _shouldCheckBeRun = _checkToolStatus.Run();

            // Assert
            Assert.IsTrue(_shouldCheckBeRun, "Date last check was ran exceeded the check frequency so this should return true.");
        }

        [TestMethod]
        public void LastCheckWasPerformedTooRecentlyToBeRepeated_ReturnsFalse()
        {
            // Arrange
            _configFileHelperStub.PrsErrorCheckFrequency = 10;   // Errors should be checked every 10 minutes
            _configFileHelperStub.TimeLastPrsErrorCheckWasRun = DateTime.Now.AddMinutes(-9);   // Last check was run 9 minutes ago
            DetermineIfPrsErrorActivityShouldBeChecked _checkToolStatus = new DetermineIfPrsErrorActivityShouldBeChecked(_configFileHelperStub, _log);
            bool _shouldCheckBeRun = true;   // Defaulting to the value I'm not expecting

            // Act
            _shouldCheckBeRun = _checkToolStatus.Run();

            // Assert
            Assert.IsFalse(_shouldCheckBeRun, "Date last checked fell within the check frequency so this should return false.");
        }
    }
}
