using PRSErrorMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace PRSErrorMonitorTests
{
    /// <summary>
    ///This is a test class for RecordPRSErrorActivityTest and is intended
    ///to contain all RecordPRSErrorActivityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RecordPRSErrorActivityTest
    {
        private IePharmacyEntities _ePharmMockEntity;
        private IReportingEntities _reportingMockEntity;
        private IRepository _mockRepository;

        private int UnableToContactPRSError = 3000;
        private int PRSTimeoutError = 3005;

        [TestInitialize]
        public void TestInitialize()
        {
            _ePharmMockEntity = new ePharmacyEntitiesMock();
            _reportingMockEntity = new ReportingEntitiesMock();
            _mockRepository = new Repository(_ePharmMockEntity, _reportingMockEntity);
        }

        [TestMethod]
        public void NoDataExistsIntbAuditExchangeInbound_ErrorCountsEqualZero()
        {
            RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_mockRepository);
            _recordPrsErrorActivity.RecordPRSErrorCounts();

            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSUnavailableErrors, 0);
            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSTimeoutErrors, 0);
        }

        [TestMethod]
        public void PrsErrorsExistOutwithCorrectTimeframe_ErrorCountsEqualZero()
        {
            DateTime _tenMinutesAgo = DateTime.Now.AddMinutes(-10);

            // Add one of each type of error but make the time they were logged too old to be picked up in this run
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(UnableToContactPRSError, _tenMinutesAgo));
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(PRSTimeoutError, _tenMinutesAgo));

            RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_mockRepository);
            _recordPrsErrorActivity.RecordPRSErrorCounts();

            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSUnavailableErrors, 0);
            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSTimeoutErrors, 0);
        }

        [TestMethod]
        public void SingleUnavailableErrorExistsInCorrectTimeframe_ErrorCountEqualsOne()
        {
            DateTime previousMinute = DateTime.Now.AddMinutes(-1);
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(UnableToContactPRSError, previousMinute));

            RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_mockRepository);
            _recordPrsErrorActivity.RecordPRSErrorCounts();

            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSUnavailableErrors, 1);
        }

        [TestMethod]
        public void SingleTimeoutErrorExistsInCorrectTimeframe_ErrorCountEqualsOne()
        {
            DateTime previousMinute = DateTime.Now.AddMinutes(-1);
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(PRSTimeoutError, previousMinute));

            RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_mockRepository);
            _recordPrsErrorActivity.RecordPRSErrorCounts();

            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSTimeoutErrors, 1);
        }

        [TestMethod]
        public void MultipleErrorsExistsInCorrectTimeframe_CorrectErrorCountsAreSaved()
        {
            DateTime previousMinute = DateTime.Now.AddMinutes(-1);

            // Add 2 Unavailable errors
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(UnableToContactPRSError, previousMinute));
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(UnableToContactPRSError, previousMinute));
            // Add 3 Timeout errors
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(PRSTimeoutError, previousMinute));
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(PRSTimeoutError, previousMinute));
            _ePharmMockEntity.tbAuditExchangeInbound.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbAuditExchangeInbound.AddTableRow(PRSTimeoutError, previousMinute));

            RecordPRSErrorActivity _recordPrsErrorActivity = new RecordPRSErrorActivity(_mockRepository);
            _recordPrsErrorActivity.RecordPRSErrorCounts();

            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSUnavailableErrors, 2);
            Assert.AreEqual(_reportingMockEntity.tbPRSErrorMonitor.ElementAt(0).PRSTimeoutErrors, 3);
        }
    }
}
