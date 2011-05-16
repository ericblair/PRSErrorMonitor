using PRSErrorMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PRSErrorMonitorTests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RetrieveLatestRowFromPrsErrorMonitorTableTests
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
        public void MultipleRowsExistInPrsErrorMonitorTable_LastRowIsReturned()
        {
            _reportingMockContext.tbRPT_PRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(DateTime.Now.AddHours(-5)));
            _reportingMockContext.tbRPT_PRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(DateTime.Now.AddHours(-1)));
            _reportingMockContext.tbRPT_PRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(DateTime.Now.AddMinutes(-30)));
            _reportingMockContext.tbRPT_PRSErrorMonitor.AddObject(TestHelpers.PopulateDatabaseTables.AddRowTotbPrsErrorMonitor.AddTableRow(DateTime.Now.AddMinutes(-5)));

            tbRPT_PRSErrorMonitor _latestRow = _repository.RetrieveLatestRowFromPrsErrorMonitorTable();

            // Deceided to take approach below rather than try to scrub off the milliseconds
            Assert.AreEqual(DateTime.Now.Date, _latestRow.DateTime.Date, "Date did not match");
            Assert.AreEqual(DateTime.Now.Hour, _latestRow.DateTime.Hour, "Hour did not match");
            Assert.AreEqual(DateTime.Now.AddMinutes(-5).Minute, _latestRow.DateTime.Minute, "Minute did not match");
            Assert.AreEqual(DateTime.Now.Second, _latestRow.DateTime.Second, "Second did not match");
        }
    }
}
