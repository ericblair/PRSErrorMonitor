using System;
namespace PRSErrorMonitor
{
    public interface ICheckPrsErrorLevels
    {
        void CheckIfPrsHasExceededErrorLimit();
    }
}
