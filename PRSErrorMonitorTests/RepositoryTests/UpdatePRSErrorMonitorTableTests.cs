using PRSErrorMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Moq;

namespace PRSErrorMonitorTests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UpdatePRSErrorMonitorTableTests
    {
        private IePharmacyEntities _ePharmacyMockContext;
        private IReportingEntities _reportingMockContext;
        private PRSErrorMonitor.Repository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _reportingMockContext = new ReportingEntitiesMock();
            _repository = new PRSErrorMonitor.Repository(_ePharmacyMockContext, _reportingMockContext);
        }

        [TestMethod]
        public void AddNewRowToPRSErrorMonitorTable_CorrectDetailsSaved()
        {
            DateTime _currentDateTime = DateTime.Now;
            int _numberOfPRSUnavailableErrors = 10;
            int _numberOfPRSTimeoutErrors = 20;

            _repository.UpdatePRSErrorMonitorTable(_currentDateTime, _numberOfPRSUnavailableErrors, _numberOfPRSTimeoutErrors);

            Assert.AreEqual(_reportingMockContext.tbPRSErrorMonitor.ElementAt(0).DateTime, _currentDateTime);
            Assert.AreEqual(_reportingMockContext.tbPRSErrorMonitor.ElementAt(0).PRSUnavailableErrors, _numberOfPRSUnavailableErrors);
            Assert.AreEqual(_reportingMockContext.tbPRSErrorMonitor.ElementAt(0).PRSTimeoutErrors, _numberOfPRSTimeoutErrors);
        }
    }
}
