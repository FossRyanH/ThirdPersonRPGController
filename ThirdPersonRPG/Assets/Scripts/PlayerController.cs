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
    Controls _controls;

    [Header("Movement")]
    public float RunSpeed = 6f;
    public float SprintSpeed = 10f;


    // Start is called before the first frame update

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
}
