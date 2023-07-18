using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UI_GameplayController : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private InputSystemUIInputModule _input;

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
        Signals.OnPause += OnPauseActivate;

    }

    private void Unsubscribe()
    {
        Signals.OnPause -= OnPauseActivate;

    }

    private void OnPauseActivate(object sender, bool value)
    {
        _menuPanel.gameObject.SetActive(value);

        if (value)
        {
            PlayerController pc = sender as PlayerController;
            if (pc != null)
            {
                PlayerInput pi = pc.GetComponent<PlayerInput>();
                if (pi == null) { return; }

                _input.actionsAsset = pi.actions;
            }
        }
    }
}
