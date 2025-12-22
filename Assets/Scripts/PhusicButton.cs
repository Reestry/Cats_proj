using Player;
using UnityEngine;

public class PhusicButton : MonoBehaviour, IInteractableItem<object>
{
    
    public object Interact()
    {
        Debug.Log("Жмал");
        return null;
    }
}
