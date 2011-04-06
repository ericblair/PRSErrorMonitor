using PRSErrorMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace PRSErrorMonitorTests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Repository_ReturnErrorCountTests
    {
        private IePharmacyEntities _ePharmacyMockContext;
        private IReportingEntities _reportingMockContext;
        private PRSErrorMonitor.Repository _repository;

        private int UnableToContactPRSError = 3000;
        private int PRSTimeoutError = 3005;


        [TestInitialize]
        public void TestInitialize()
        {
            _ePharmacyMockContext = new ePharmacyEntitiesMock();
            _repository = new PRSErrorMonitor.Repository(_ePharmacyMockContext, _reportingMockContext);
        }

        [TestMethod]
        public void NoPRSErrorsHaveOccured_ReturnsZero()
        {
            int numberOfErrorsFound = 0;
            DateTime previousMinute = DateTime.Now.AddMinutes(-1);

            numberOfErrorsFound = _repository.ReturnErrorCountInPreviousMinute(previousMinute, UnableToContactPRSError);
            numberOfErrorsFound += _repository.ReturnErrorCountInPreviousMinute(previousMinute, PRSTimeoutError);

            Assert.AreEqual(0, numberOfErrorsFound);
        }

        [TestMethod]
        public void SingleUnableToContactPRSErrorHasOccured_ReturnsOne()
        {
            int numberOfErrorsFound = 0;
            DateTime previousMinute = DateTime.Now.AddMinutes(-1);

            _ePharmacyMockContext.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(UnableToContactPRSError, previousMinute));

            numberOfErrorsFound = _repository.ReturnErrorCountInPreviousMinute(previousMinute, UnableToContactPRSError);

            Assert.AreEqual(1, numberOfErrorsFound);
        }

        [TestMethod]
        public void SinglePRSTimeoutErrorHasOccured_ReturnsOne()
        {
            int numberOfErrorsFound = 0;
            DateTime previousMinute = DateTime.Now.AddMinutes(-1);

            _ePharmacyMockContext.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(PRSTimeoutError, previousMinute));

            numberOfErrorsFound = _repository.ReturnErrorCountInPreviousMinute(previousMinute, PRSTimeoutError);

            Assert.AreEqual(1, numberOfErrorsFound);
        }
    }
}
