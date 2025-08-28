using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    private Transform character;
    public float sensitivity = 2f;
    public float smoothing = 1.5f;

    public InputActionReference lookAction;

    private Vector2 velocity;
    private Vector2 frameVelocity;

    void OnEnable()
    {
        lookAction.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        lookAction.action.Disable();
    }

    void Update()
    {
        Vector2 mouseDelta = lookAction.action.ReadValue<Vector2>();
        Vector2 rawFrameVelocity = mouseDelta * sensitivity;
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1f / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>()?.transform;
    }
}