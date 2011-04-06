using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRSErrorMonitor;

namespace PRSErrorMonitorTests.TestHelpers.PopulateDatabaseTables
{
    public static class AddRowTotbAuditExchangeInbound
    {
        public static tbAuditExchangeInbound AddTableRow()
        {
            tbAuditExchangeInbound row = new tbAuditExchangeInbound
            {
                rid = 1,
                beginTime = DateTime.Now,
                endTime = DateTime.Now,
                requestMessageId = "TEST",
                responseMessageId = "TEST",
                responseCorrId = "TEST",
                requestSource = "TEST",
                requestDestination = "TEST",
                responseSource = "TEST",
                responseDestination = "TEST",
                sourceUrl = "TEST",
                clientIpAddress = "TEST",
                serverMachineName = "TEST",
                authenticated = true,
                authCertSerialNumber = "TEST",
                authCertSubject = "TEST",
                soapFaultType = "TEST",
                faultMessage = "TEST",
                auditCreatedOn = DateTime.Now,
                auditCreatedBy = "TEST",
                auditUpdatedOn = DateTime.Now,
                auditUpdatedBy = "TEST",
                requestCorrID = "TEST",
                clientDiagAdapterVersion = "TEST",
                clientDiagDBVersion = "TEST",
                clientDiagOSVersion = "TEST",
                clientDiagDotNetVersion = "TEST",
                clientDiagProcessName = "TEST",
                clientDiagMsgsWaitingToBePosted = 0,
                clientDiagMsgsWaitingRetry = 0,
                clientDiagMsgsFailed = 0,
                clientDiagMsgsToBeDequeued = 0,
                clientDiagOutboundAuditCount = 0,
                clientDiagInboundAuditCount = 0,
                trainingAppTransId = new Guid(),
                trainingAppTransStepRef = 0,
                trainingOrganisation = "TEST",
                trainingOrganisationType = "TEST",
                trainingMsgType = "TEST",
                trainingMsgVersion = "TEST",
                trainingMsgIdentifiedBy = "TEST",
                requestMessageIdStrong = new Guid(),
                diagReference = new Guid(),
                diagNumber = 0,
                diagType = "TEST",
                diagMessage = "TEST",
                diagDetail = "TEST",
                diagRequestXml = "TEST"
            };

            return row;
        }

        public static tbAuditExchangeInbound AddTableRow(int errorCode, DateTime auditCreatedOn)
        {
            tbAuditExchangeInbound row = AddTableRow();

            row.diagNumber = errorCode;
            row.auditCreatedOn = auditCreatedOn;

            return row;
        }
    }
}
