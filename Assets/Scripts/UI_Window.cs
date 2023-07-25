using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Window : MonoBehaviour
{
    [SerializeField] private string _windowName;
    public string WindowName { get { return _windowName; } }

    [SerializeField] private GameObject _root;
    [SerializeField] private CanvasGroup _cg;
    [SerializeField] private Selectable _firstSelection;
    [SerializeField] private bool _rememberLastSelection;

    private Selectable _lastSelection;

    public virtual void Open()
    {
        _root.gameObject.SetActive(true);
        ActivateInteraction();
    }

    public virtual void Close()
    {
        DeactivateInteraction();
        _root.gameObject.SetActive(false);
    }

    public void ActivateInteraction()
    {
        _cg.interactable = true;

        if (_rememberLastSelection && _lastSelection != null) { _lastSelection.Select(); }
        else if (_firstSelection != null) { _firstSelection.Select(); }
    }

    public void DeactivateInteraction()
    {
        _cg.interactable = false;
    }
}
