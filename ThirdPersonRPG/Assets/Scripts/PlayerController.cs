using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    [SerializeField]
    GameObject _gameManager;
    [SerializeField]
    GameObject _playerUI;
    public Rigidbody Rb;
    [SerializeField]
    Health _health;

    public Vector2 MovementValue = new Vector2();
    public event Action RunEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;
    public event Action PauseEvent;

    // Change this for an event later to create a combo chain reset if button isn't pressed in "x" amount of time.
    public bool IsAttacking { get; private set; }

    Controls _controls;
    int _score;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();

        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _playerUI = GameObject.FindGameObjectWithTag("PlayerHealthBar");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _score = _gameManager.GetComponent<GameManager>().Score;

        _health = GetComponent<Health>();

        UpdateMaxHealth(_health.MaxHealth);
    }

    void Update()
    {
        UpdateHealth(_health.CurrentHealth);
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

    public void AddScore(int score)
    {
        _score = score;
        _gameManager.GetComponent<GameManager>()?.SetScore(_score);
    }

    public void UpdateHealth(float currentHealth)
    {
        _playerUI.GetComponent<HPElements>().SetHealthLevel(currentHealth);
    }

    void UpdateMaxHealth(float maxHealth)
    {
        _playerUI.GetComponent<HPElements>().SetHealthMaxValue(maxHealth);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        PauseEvent?.Invoke();
    }
}
