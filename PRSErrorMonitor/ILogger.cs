using System;

namespace PRSErrorMonitor
{
    public interface ILogger
    {
        void Add(string message);
        void Write();
    }
}