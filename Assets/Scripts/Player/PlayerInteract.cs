using UnityEngine;

public class PlayerInteract : MonoBehaviour, IInputable
{
    private Holder _holder;

    private InputSystem_Actions _inputSystemActions;
    private PlacingHeight _placingHeight;

    public Vector3 halfExtents = new(2, 2, 2);
    
    
    float minDistance = Mathf.Infinity;
    
    
    private void Start()
    {
        GetComponent<PlayerController>()?.SetInput(this);
        _placingHeight = GetComponentInChildren<PlacingHeight>();
        _holder = GetComponentInChildren<Holder>();
    }
    
    
    public void Run()
    {
        if (!_inputSystemActions.Player.Interact.WasPressedThisFrame())
            return;
        
        if (_holder.HasItem())
        {
            DropItem();
            return;
        }
        
        var col = Physics.OverlapBox(_holder.transform.position, halfExtents, 
            Quaternion.Euler(_holder.transform.rotation.x, _holder.transform.rotation.y, _holder.transform.rotation.z),
            LayerMask.GetMask ("Default"),
            QueryTriggerInteraction.Collide);

        foreach (var collider in col)
        {
            var obj = collider?.GetComponent<Iinteractable>();
            if (obj is not Iinteractable)
                continue;

            if (obj is Item item)
            {
                TakeItem(item);
                return;
            }
            
            obj.Interact();
            return;
        }
    }

    private void TakeItem(Item item)
    {
        _holder.SetItem(item);
    }
    
    private void DropItem()
    {
        var dropedItem = _holder.UnsetItem();
        dropedItem.transform.position = _placingHeight.DropPlace() + new Vector3(0,0.5f,0);
    }

    public void Init()
    {
        _inputSystemActions = new InputSystem_Actions();
        _inputSystemActions.Enable();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_holder.transform.position, halfExtents * 2);
    }
}

