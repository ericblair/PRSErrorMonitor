//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// Architectural overview and usage guide: 
// http://blogofrab.blogspot.com/2010/08/maintenance-free-mocking-for-unit.html
//------------------------------------------------------------------------------
using System.Data.EntityClient;
using System.Data.Objects;
using PRSErrorMonitor.ReportingEntitiesMockObjectSet;

namespace PRSErrorMonitor
{
    /// <summary>
    /// The concrete mock context object that implements the context's interface.
    /// Provide an instance of this mock context class to client logic when testing, 
    /// instead of providing a functional context object.
    /// </summary>
    public partial class ReportingEntitiesMock : IReportingEntities
    {
        public IObjectSet<tbRPT_PRSErrorMonitor> tbRPT_PRSErrorMonitor
        {
            get { return _tbRPT_PRSErrorMonitor  ?? (_tbRPT_PRSErrorMonitor = new MockObjectSet<tbRPT_PRSErrorMonitor>()); }
        }
        private IObjectSet<tbRPT_PRSErrorMonitor> _tbRPT_PRSErrorMonitor;

        public int SaveChanges() { return 0; } 
    }
}
