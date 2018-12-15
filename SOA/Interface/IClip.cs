using System;

namespace SOA.Interface
{
    public interface IClip : IDelay<IClip>
    {
        void Copy();

        void Paste();

        void SetText(string text);

        string GetText();
    }
}
