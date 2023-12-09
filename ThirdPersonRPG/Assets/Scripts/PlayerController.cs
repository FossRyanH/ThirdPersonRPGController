using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    GameManager _gameManager;
    public Rigidbody Rb;

    public Vector2 MovementValue = new Vector2();
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action RunEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;

    // Change this for an event later to create a combo chain reset if button isn't pressed in "x" amount of time.
    public bool IsAttacking { get; private set; }

    Controls _controls;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // 
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        RunEvent?.Invoke();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CancelEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }
}
