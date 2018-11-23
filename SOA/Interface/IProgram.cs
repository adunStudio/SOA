using System;
using System.Diagnostics;

namespace SOA.Interface
{
    public interface IProgram : IInit
    {
        void Show(string processName);

        void Hide(string processName);

        void Start(string processName);

        void Exit(string processName);
    }
}
