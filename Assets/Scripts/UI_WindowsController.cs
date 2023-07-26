using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_WindowsController : MonoBehaviour
{
    public event EventHandler<string> OnWindowOpened;
    public event EventHandler<string> OnWindowClosed;

    [SerializeField] private UI_Window[] _windows;
    private List<UI_Window> _active = new List<UI_Window>();

    public int ActiveWindowsCount { get { return _active.Count; } }

    public UI_Window GetWindow(string name)
    {
        for (int i = 0; i < _windows.Length; i++)
        {
            if (_windows[i] == null) { continue; }
            if (_windows[i].WindowName != name) { continue; }

            return _windows[i];
        }

        return null;
    }

    public UI_Window GetActiveWindow(string name)
    {
        for (int i = 0; i < _active.Count; i++)
        {
            if (_active[i] == null) { continue; }
            if (_active[i].WindowName != name) { continue; }

            return _active[i];
        }

        return null;
    }

    public bool IsWindowOpened(string name)
    {
        for (int i = 0; i < _active.Count; i++)
        {
            if (_active[i] == null) { continue; }
            if (_active[i].WindowName != name) { continue; }

            return true;
        }

        return false;
    }

    public void OpenWindow(string name)
    {
        UI_Window w = GetActiveWindow(name);

        for (int i = 0; i < _windows.Length; i++)
        {
            if (_windows[i] == null) { continue; }
            if (_windows[i].WindowName != name) { continue; }

            w = _windows[i];
            break;
        }

        if (w == null) { return; }
        if (_active.Contains(w)) { return; }
        
        for (int i = 0; i < _active.Count; i++)
        {
            if (_active[i] == null) { continue; }
            _active[i].DeactivateInteraction();
        }

        w.Open();
        w.transform.SetAsLastSibling();

        _active.Add(w);
        OnWindowOpened?.Invoke(this, w.WindowName);
    }

    private void ActivateLast()
    {
        if (_active.Count == 0) { return; }

        UI_Window w = _active[_active.Count - 1];
        w.ActivateInteraction();
        w.transform.SetAsLastSibling();
    }

    public void Close(string name)
    {
        UI_Window w = GetActiveWindow(name);
        if (w == null) { return; }
        
        w.Close();
        _active.Remove(w);
        OnWindowClosed?.Invoke(this, w.WindowName);

        ActivateLast();
    }

    public void CloseHot(string name)
    {
        UI_Window w = GetActiveWindow(name);
        if (w == null) { return; }

        w.HotClose();
        _active.Remove(w);
        OnWindowClosed?.Invoke(this, w.WindowName);

        ActivateLast();
    }

    public void CloseNext(bool hot = false)
    {
        if (_active.Count == 0) { return; }
        UI_Window w = _active[_active.Count - 1];

        if (hot) { CloseHot(w.WindowName); }
        else { Close(w.WindowName); }
    }

    public void CloseAllWindows()
    {
        if (_active.Count == 0) { return; }
        int count = _active.Count;

        for (int i = 0; i < count; i++)
        {
            CloseNext(true);
        }
    }
}
