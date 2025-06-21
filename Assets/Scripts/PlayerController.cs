using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float mouseSensitivity;
    //public Transform cameraTransform;
    public InputActionAsset actionAsset;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private Vector2 _movement;
    private Vector2 _lookInput;
    //private float _xRotation = 0.0f;

    private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        var map = actionAsset.FindActionMap("Player");
        _moveAction = map.FindAction("Move");
        _lookAction = map.FindAction("Look");
        
        _moveAction.Enable();
        _lookAction.Enable();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _movement = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();
        
        //transform.Rotate(Vector3.up * (_lookInput.x * mouseSensitivity * -1f));
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Ignore tilt
        transform.forward = cameraForward;

    }

    void FixedUpdate()
    {
        Vector3 moveDirection = transform.right * _movement.x + transform.forward * _movement.y;
        Vector3 velocity = moveDirection.normalized * speed;
        
        velocity.y = _rb.linearVelocity.y;
        
        _rb.linearVelocity = velocity;
    }

    // Update is called once per frame
}
