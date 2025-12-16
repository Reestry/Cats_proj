using UnityEngine;

public class MouseTake : MonoBehaviour
{
    [SerializeField] private Transform _handPos;
    [SerializeField] private float _force = 500;
    
    private MouseInput _mouseInput;
    private Rigidbody _item;
    
    private void Start()
    {
        _mouseInput = GetComponent<MouseInput>();

        _mouseInput.OnObjectTaken += SetItem;
        _mouseInput.OnObjectReleased += ReleaseItem;

        _mouseInput.OnZoomScrolled += Zoom;
    }

    private void Zoom(float zoomVal)
    {
        Debug.Log(zoomVal);
        
        if (zoomVal == 0)
            return;

        var direction = (_handPos.position - transform.position).normalized;


        float step = zoomVal > 0 ? 1f : -1f;

        _handPos.position += direction * step ;
    }

    void Update()
    {
        
        if (_item == null)
            return;
        
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(_mouseInput.ReturnMousePos().x,
            _mouseInput.ReturnMousePos().y,
          Camera.main.WorldToScreenPoint(_handPos.position).z ));

        _item.linearVelocity = (mousePos - _item.transform.position) * _force * Time.deltaTime;
        
    }

    private void SetItem(Rigidbody obj)
    {
        if (_item != null)
            return;
        
        _item = obj;
        _item.useGravity = false;
    }

    private void ReleaseItem()
    {
        if (_item == null)
            return;

        _item.useGravity = true;
        _item = null;
    }
}
