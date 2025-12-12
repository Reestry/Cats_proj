using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event Action<Rigidbody> OnObjectTaken;
    public event Action OnObjectReleased;
    
    private InputSystem_Actions _inputActions;
    private Vector2 _mousePos; 
    float currentTime;
    
    public Vector2 ReturnMousePos()
    {
        return _mousePos;
    }
    
    private void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
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
            }
        }
        
        if (_inputActions.Interactable.Interact.WasReleasedThisFrame())
        {
            OnObjectReleased?.Invoke();
        }

        currentTime += Time.deltaTime;
        
        if (currentTime >= 5)
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
