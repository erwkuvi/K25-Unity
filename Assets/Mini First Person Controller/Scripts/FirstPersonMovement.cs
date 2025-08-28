using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5f;

    [Header("Running")]
    public bool canRun = true;
    public float runSpeed = 9f;
    public InputActionReference moveAction;
    public InputActionReference runAction;

    public bool IsRunning { get; private set; }

    private Rigidbody rb;

    public List<System.Func<float>> speedOverrides = new();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        runAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        runAction.action.Disable();
    }

    void FixedUpdate()
    {
        IsRunning = canRun && runAction.action.ReadValue<float>() > 0.1f;

        float currentSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
            currentSpeed = speedOverrides[speedOverrides.Count - 1]();

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = transform.rotation * new Vector3(input.x, 0, input.y) * currentSpeed;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}