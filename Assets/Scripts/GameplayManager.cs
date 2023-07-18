using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private InputAction _playerInput;

    private bool _gameIsPaused;

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
        Signals.IsPaused = () => { return _gameIsPaused; };
        Signals.OnPause += OnGamePause;
        Signals.GameplayDeltaTime = () => { return _gameIsPaused ? 0 : Time.deltaTime; };
    }

    private void Unsubscribe()
    {
        Signals.IsPaused = null;
        Signals.OnPause -= OnGamePause;
    }

    private void OnGamePause(object sender, bool e)
    {
        _gameIsPaused = e;
    }
}
