using UnityEngine;

public class MouseTake : MonoBehaviour
{
    [SerializeField] private Transform _handPos;
    [SerializeField] private float _force = 500;
    
    private MouseInput _mouseInput;
    private Rigidbody _item;
    private Vector3 _lastHandPos;
    
    private void Start()
    {
        _mouseInput = GetComponent<MouseInput>();

        _mouseInput.OnObjectTaken += SetItem;
        _mouseInput.OnObjectReleased += ReleaseItem;

        _mouseInput.OnZoomScrolled += Zoom;
        _mouseInput.OnObjectFreezed += FreezeObject;
        _mouseInput.OnResetRotationPressed += ResetRotation;
    }

    private void Zoom(float zoomVal)
    {
        if (zoomVal == 0)
            return;

        var direction = (_handPos.position - transform.position).normalized;

        if ((_handPos.position - transform.position).magnitude > 2)
        {
            _lastHandPos = _handPos.position;
            var step = zoomVal > 0 ? 1f : -1f;
            _handPos.position += direction * step ;
            
        }
        else
            _handPos.position = _lastHandPos;

    }

    private void Update()
    {
        if (_item == null)
            return;
        
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(_mouseInput.ReturnMousePos().x,
            _mouseInput.ReturnMousePos().y,
          Camera.main.WorldToScreenPoint(_handPos.position).z));

        _item.linearVelocity = (mousePos - _item.transform.position) * _force * Time.deltaTime;
    }

    private void FreezeObject()
    {
        _item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ReleaseItem();
    }

    private void ResetRotation()
    {
        _item.transform.rotation = Quaternion.Euler(Vector3.zero);
        _item.freezeRotation = true;
        _item.freezeRotation = false;
    }

    private void SetItem(Rigidbody obj)
    {
        if (_item != null)
            return;
        
        _item = obj;
        _item.constraints = RigidbodyConstraints.None;
        _item.useGravity = false;

    }

    private void ReleaseItem()
    {
        if (_item == null)
            return;

        _item.useGravity = true;
        _item = null;
    }

    private void OnDisable()
    {
        _mouseInput.OnObjectTaken -= SetItem;
        _mouseInput.OnObjectReleased -= ReleaseItem;

        _mouseInput.OnZoomScrolled -= Zoom;
        _mouseInput.OnObjectFreezed -= FreezeObject;
        _mouseInput.OnResetRotationPressed -= ResetRotation;
    }
}
