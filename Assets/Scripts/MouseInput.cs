using System;
using Player;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event Action<Rigidbody> OnObjectTaken;
    public event Action OnObjectReleased;
    public event Action<float> OnZoomScrolled;
    
    private InputSystem_Actions _inputActions;
    private Vector2 _mousePos; 
    private float currentTime;
    private float _zoomVal;
    
    
    public Vector2 ReturnMousePos()
    {
        return _mousePos;
    }
    
    private void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();

        _inputActions.Interactable.Zoom.performed += ctx =>
        {
            _zoomVal = _inputActions.Interactable.Zoom.ReadValue<Vector2>().y;

            if (_zoomVal == 0) return;
            
            OnZoomScrolled?.Invoke(_zoomVal);
            
        };
    }
    
    private void Update()
    {
        _mousePos = _inputActions.Interactable.MousePosition.ReadValue<Vector2>();
        
        if (_inputActions.Interactable.Interact.WasPressedThisFrame())
        {
            var ray = Camera.main.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out var hit))
            {
                if(hit.collider.TryGetComponent<Rigidbody>(out var obj))
                    OnObjectTaken?.Invoke(obj);

                if (hit.collider.TryGetComponent<IInteractableItem<Rigidbody>>(out var item))
                {
                    var interactedObject = item.Interact();

                    if (interactedObject != null)
                        OnObjectTaken?.Invoke(interactedObject.GetComponent<Rigidbody>());

                }
                
                if (hit.collider.TryGetComponent<BoxWithItems>(out var box))
                {
                    try
                    {
                        OnObjectTaken?.Invoke(box.GetItem());
                    }
                    catch
                    {
                        Debug.Log("Ничего нет :((");
                        box.DeleteBox();
                        return;
                    }
                }
            }
        }
        
        if (_inputActions.Interactable.Interact.WasReleasedThisFrame())
        {
            OnObjectReleased?.Invoke();
        }

        
        currentTime += Time.deltaTime;
        
        if (currentTime >= 5    )
        {
            currentTime = 0;
            Debug.Log(_inputActions.Interactable.MousePosition.ReadValue<Vector2>());
        }
        
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
}
