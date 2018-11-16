using System;

namespace KKK.Interface
{
    public interface ICommand : IInit
    {
        bool visible
        {
            get;
            set;
        }

        void SetVisible(bool visible);
    }
}
