using UnityEngine;

public abstract class Item : MonoBehaviour, Iinteractable
{
    private int _cost;
    private Vector3 _spawnPos;
    
    private void OnEnable()
    {
        
    }
    
    public virtual void Interact()
    {
        Debug.Log("Нашелся");
    }
    
    
}
