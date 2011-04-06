using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRSErrorMonitor
{
    partial class Repository
    {
        private IePharmacyEntities _ePharmacyEntityContext;
        private IReportingEntities _reportingEntityContext;

        public Repository(IePharmacyEntities ePharmacyContext, IReportingEntities reportingContext)
        {
            _ePharmacyEntityContext = ePharmacyContext;
            _reportingEntityContext = reportingContext;
        }
    }
}
