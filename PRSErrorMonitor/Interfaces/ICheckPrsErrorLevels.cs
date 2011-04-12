using System;
using System.Collections.Generic;
namespace PRSErrorMonitor
{
    public interface ICheckPrsErrorLevels
    {
        bool PrsIssueHasAlreadyBeenReported();
        bool PrsUnavailableErrorLevelExceeded();
        bool PrsTimeoutErrorLevelExceeded();
        bool PrsTotalErrorLevelExceeded();
    }
}
