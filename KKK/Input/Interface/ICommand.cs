using System;

namespace KKK.Input
{
    public interface ICommand
    {
        bool visible
        {
            get;
            set;
        }

        void SetVisible(bool visible);
    }
}
