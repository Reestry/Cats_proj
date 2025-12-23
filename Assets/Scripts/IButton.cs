using System;

namespace DefaultNamespace
{
    public interface IButton
    {
        public event Action OnButtonClick;
    }
}