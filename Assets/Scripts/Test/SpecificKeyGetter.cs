using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecificKeyGetter : MonoBehaviour
{
    public class ActionBinding
    {
        public string ControlScheme { get; }
        public string ActionName { get; }
        public string Device { get; }
        public string Key { get; }

        public ActionBinding(string controlScheme, string actionName, string device, string key)
        {
            ControlScheme = controlScheme;
            ActionName = actionName;
            Device = device;
            Key = key;
        }

        public override string ToString()
        {
            return Device + ": " + Key;
        }
    }


    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private string _actionMapName;
    [SerializeField] private string _schemeName;
    [SerializeField] private string _actionName;
    [SerializeField] private string _actionPartName;

    // Start is called before the first frame update
    void Start()
    {
        ActionBinding[] bindings = GetActionKey();
        for (int i = 0; i < bindings.Length; i++)
        {
            UnityEngine.Debug.Log(bindings[i].ToString());
        }
    }

    public ActionBinding[] GetActionKey()
    {
        InputControlScheme? controlScheme = null;
        foreach (var scheme in _inputActionAsset.controlSchemes)
        {
            if (scheme.name == _schemeName)
            {
                controlScheme = scheme;
                break;
            }
        }

        if (controlScheme == null)
        {
            Debug.LogError("Control scheme not found: " + _schemeName);
            return null;
        }

        // Get the action map by the given Action Map name
        InputActionMap actionMap = _inputActionAsset.FindActionMap(_actionMapName);

        if (actionMap == null)
        {
            Debug.LogError("Action map not found: " + _actionMapName);
            return null;
        }

        // Find the action in the action map by name
        InputAction action = actionMap.FindAction(_actionName, true);

        if (action != null)
        {
            List<ActionBinding> result = new List<ActionBinding>();
            // Filter the bindings using the control scheme's binding group

            for (int i = 0; i < action.bindings.Count; i++)
            {
                InputBinding binding = action.bindings[i];

                if (binding.isPartOfComposite)
                {
                    string compositeName = binding.name;
                    if (_actionPartName != compositeName) { continue; }
                }

                if (binding.groups.Contains(controlScheme.Value.bindingGroup))
                {
                    // Get the key code from the binding
                    //string keyName = binding.ToDisplayString();

                    var displayString = string.Empty;
                    var deviceLayoutName = default(string);
                    var controlPath = default(string);

                    displayString = action.GetBindingDisplayString(i, out deviceLayoutName, out controlPath, new InputBinding.DisplayStringOptions());

                    ActionBinding item = new ActionBinding(_schemeName, _actionName, deviceLayoutName, controlPath);
                    result.Add(item);
                }
            }
            return result.ToArray();
        }
        else
        {
            Debug.LogError("Action not found in the specified Action Map and Control Scheme: " + _actionName + ", " + _schemeName);
            return null;
        }
    }
}
