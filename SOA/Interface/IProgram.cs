using System;
using System.Diagnostics;

namespace SOA.Interface
{
    public interface IProgram : IInit
    {
        void Close(string processName);
    }
}
