using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private float _speed;
    [SerializeField] private string _voice;

    [SerializeField] private PlayerInput _controller;
    private Vector2 _direction;

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
        Signals.OnPause?.Invoke(this, !isPaused);

        if (!Signals.IsPaused.SafeInvoke())
        {
            _controller.SwitchCurrentActionMap("InGamePlayer");
        }
        else
        {
            _controller.SwitchCurrentActionMap("UI");
        }
    }
}
