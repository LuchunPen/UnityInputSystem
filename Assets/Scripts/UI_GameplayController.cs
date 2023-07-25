using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UI_GameplayController : MonoBehaviour
{
    [SerializeField] private InputSystemUIInputModule _input;
    [SerializeField] private UI_WindowsController _menuControllers;
    [SerializeField] private string _pauseMenuName;

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
        Signals.CloseMenuWindow = CloseMenuWindow;
        Signals.OpenMenuWindow = OpenMenuWindow;

        _menuControllers.OnWindowClosed += OnWindowClosedHandler;
    }

    private void Unsubscribe()
    {
        Signals.CloseMenuWindow = null;
        Signals.OpenMenuWindow = null;
        
        _menuControllers.OnWindowClosed -= OnWindowClosedHandler;
    }

    public int CloseMenuWindow(object sender)
    {
        _menuControllers.CloseNext();
        return _menuControllers.ActiveWindowsCount;
    }

    private void OnWindowClosedHandler(object sender, string name)
    {
        if (_menuControllers.ActiveWindowsCount == 0)
        {
            Signals.TryToUnpause?.Invoke(this, null);
        }
    }

    private int OpenMenuWindow(object sender)
    {
        PlayerController pc = sender as PlayerController;
        if (pc != null)
        {
            PlayerInput pi = pc.GetComponent<PlayerInput>();
            if (pi == null) { return 0; }

            _input.actionsAsset = pi.actions;
        }

        _menuControllers.OpenWindow(_pauseMenuName);
        return _menuControllers.ActiveWindowsCount;
    }
}
