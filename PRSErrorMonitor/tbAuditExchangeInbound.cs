//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PRSErrorMonitor
{
    public partial class tbAuditExchangeInbound
    {
        #region Primitive Properties
    
        public virtual long rid
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> beginTime
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> endTime
        {
            get;
            set;
        }
    
        public virtual string requestMessageId
        {
            get;
            set;
        }
    
        public virtual string responseMessageId
        {
            get;
            set;
        }
    
        public virtual string responseCorrId
        {
            get;
            set;
        }
    
        public virtual string requestSource
        {
            get;
            set;
        }
    
        public virtual string requestDestination
        {
            get;
            set;
        }
    
        public virtual string responseSource
        {
            get;
            set;
        }
    
        public virtual string responseDestination
        {
            get;
            set;
        }
    
        public virtual string sourceUrl
        {
            get;
            set;
        }
    
        public virtual string clientIpAddress
        {
            get;
            set;
        }
    
        public virtual string serverMachineName
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> authenticated
        {
            get;
            set;
        }
    
        public virtual string authCertSerialNumber
        {
            get;
            set;
        }
    
        public virtual string authCertSubject
        {
            get;
            set;
        }
    
        public virtual string soapFaultType
        {
            get;
            set;
        }
    
        public virtual string faultMessage
        {
            get;
            set;
        }
    
        public virtual System.DateTime auditCreatedOn
        {
            get;
            set;
        }
    
        public virtual string auditCreatedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> auditUpdatedOn
        {
            get;
            set;
        }
    
        public virtual string auditUpdatedBy
        {
            get;
            set;
        }
    
        public virtual string requestCorrID
        {
            get;
            set;
        }
    
        public virtual string clientDiagAdapterVersion
        {
            get;
            set;
        }
    
        public virtual string clientDiagDBVersion
        {
            get;
            set;
        }
    
        public virtual string clientDiagOSVersion
        {
            get;
            set;
        }
    
        public virtual string clientDiagDotNetVersion
        {
            get;
            set;
        }
    
        public virtual string clientDiagProcessName
        {
            get;
            set;
        }
    
        public virtual Nullable<int> clientDiagMsgsWaitingToBePosted
        {
            get;
            set;
        }
    
        public virtual Nullable<int> clientDiagMsgsWaitingRetry
        {
            get;
            set;
        }
    
        public virtual Nullable<int> clientDiagMsgsFailed
        {
            get;
            set;
        }
    
        public virtual Nullable<int> clientDiagMsgsToBeDequeued
        {
            get;
            set;
        }
    
        public virtual Nullable<int> clientDiagOutboundAuditCount
        {
            get;
            set;
        }
    
        public virtual Nullable<int> clientDiagInboundAuditCount
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> trainingAppTransId
        {
            get;
            set;
        }
    
        public virtual Nullable<int> trainingAppTransStepRef
        {
            get;
            set;
        }
    
        public virtual string trainingOrganisation
        {
            get;
            set;
        }
    
        public virtual string trainingOrganisationType
        {
            get;
            set;
        }
    
        public virtual string trainingMsgType
        {
            get;
            set;
        }
    
        public virtual string trainingMsgVersion
        {
            get;
            set;
        }
    
        public virtual string trainingMsgIdentifiedBy
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> requestMessageIdStrong
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> diagReference
        {
            get;
            set;
        }
    
        public virtual Nullable<int> diagNumber
        {
            get;
            set;
        }
    
        public virtual string diagType
        {
            get;
            set;
        }
    
        public virtual string diagMessage
        {
            get;
            set;
        }
    
        public virtual string diagDetail
        {
            get;
            set;
        }
    
        public virtual string diagRequestXml
        {
            get;
            set;
        }

        #endregion
    }
}
