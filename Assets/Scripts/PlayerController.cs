using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private float _speed;
    [SerializeField] private string _voice;

    [SerializeField] private PlayerInput _controller;
    private Vector2 _direction;

    private void Awake()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        Signals.OnPause += OnPauseHandler;
    }

    private void Unsubscribe()
    {
        Signals.OnPause -= OnPauseHandler;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _playerTrans.position += (Vector3)(_direction * _speed * Signals.GameplayDeltaTime?.Invoke());
    }

    private void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }

    private void OnVoice(InputValue value)
    {
        UnityEngine.Debug.Log(_voice);
    }

    private void OnTogglePause(InputValue value)
    {
        bool isPaused = Signals.IsPaused.SafeInvoke();

        if (isPaused) { Signals.TryToUnpause?.Invoke(this, null); }
        else { Signals.TryToPause?.Invoke(this, null); }
    }

    private void OnPauseHandler(object sender, bool e)
    {
        if (e) { _controller.SwitchCurrentActionMap("UI"); }
        else { _controller.SwitchCurrentActionMap("InGamePlayer"); }
    }
}
