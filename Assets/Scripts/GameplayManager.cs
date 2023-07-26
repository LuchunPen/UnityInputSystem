using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerInput;
    private PlayerController _pauseInitiator = null;
    private bool _gameIsPaused;

    private void Awake()
    {
        Subscribe();
        
        RebindHelper.LoadBindings(_playerInput);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        Signals.IsPaused = () => { return _gameIsPaused; };
        Signals.GameplayDeltaTime = () => { return _gameIsPaused ? 0 : Time.deltaTime; };
        Signals.TryToPause += OnTryToPauseHandler;
        Signals.TryToUnpause += OnTryToUnpauseHandler;

    }

    private void Unsubscribe()
    {
        Signals.IsPaused = null;
    }

    private void OnTryToPauseHandler(object sender, EventArgs e)
    {
        if (_gameIsPaused) { return; }

        PlayerController pl = sender as PlayerController;
        if (pl != null)
        {
            int openedMenuCount = Signals.OpenMenuWindow.SafeInvoke(pl);
            if (openedMenuCount > 0)
            {
                _pauseInitiator = pl;
                _gameIsPaused = true;

                Signals.OnPause(_pauseInitiator, true);
            }
        }
    }

    private void OnTryToUnpauseHandler(object sender, EventArgs e)
    {
        if (!_gameIsPaused) { return; }

        PlayerController pl = sender as PlayerController;
        if (pl == null || (pl != null && _pauseInitiator == pl))
        {
            int openedMenuCount = Signals.CloseMenuWindow.SafeInvoke(pl);
            if (openedMenuCount == 0)
            {
                _gameIsPaused = false;
                _pauseInitiator = null;

                Signals.OnPause(pl, false);
            }
        }
    }
}
