using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IInputable
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _playerObj;
    [SerializeField] private float _pushForce = 5;
    
    private PlayerController _playerController;
    private CharacterController _controller;
    private InputSystem_Actions _inputSystemActions;

    private HandsAnimation _handsAnimation;

    private Quaternion _lookAngle;
    private Vector3 _gravityVelocity;
    private const float _gravity = -9.8f;

    private void Start()
    {
        GetComponent<PlayerController>()?.SetInput(this);

        _handsAnimation = GetComponent<HandsAnimation>();
    }
    
    private void OnDisable()
    {
        _inputSystemActions.Disable();
    }

    //Update
    public void Run()
    {
        Move();
    }

    public event Action OnMoveAnimation;

    private void Move()
    {
        var movement = _inputSystemActions.Player.Move.ReadValue<Vector2>();
        _controller.Move((movement.x * transform.right + movement.y * transform.forward) 
                         * _speed * Time.deltaTime);
        
        var look = (movement.x * transform.right + movement.y * transform.forward).normalized;
        
        if (look == Vector3.zero)
            return;
        OnMoveAnimation?.Invoke();
        
        _lookAngle = Quaternion.LookRotation(look);
        _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, _lookAngle, 10 * Time.deltaTime);
        
        
        //_handsAnimation.Shake();
    }


    private void Update()
    {
        Gravity();
    }

    private void Gravity()
    {
        _gravityVelocity.y += _gravity * Time.deltaTime;

        _controller.Move(_gravityVelocity * Time.deltaTime);

        if (_controller.isGrounded && _gravityVelocity.y <= 0)
            _gravityVelocity.y = -2f;
        
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var obj = hit.collider.attachedRigidbody;
        
        if (obj == null || obj.isKinematic)
            return;
        
        
        obj.AddForce(hit.moveDirection * _pushForce * Time.deltaTime);
    }

    public void Init()
    {
        _inputSystemActions = new InputSystem_Actions();
        _inputSystemActions.Enable();
        _controller = GetComponentInChildren<CharacterController>();
    }
}
