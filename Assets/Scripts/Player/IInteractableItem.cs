using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public interface IInteractableItem<T> where T : class
    {
        public T Interact();

    }
}