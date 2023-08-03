using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class InputActionKeyGetter : MonoBehaviour
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
                return ActionName + " - " + Device + ": " + Key;
            }
        }


        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private string _actionMapName;
        [SerializeField] private string _schemeName;
        [SerializeField] private string _actionName;

        public ActionBinding[] GetActionKeys()
        {
            InputControlScheme? controlScheme = null;
            List<ActionBinding> result = new List<ActionBinding>();

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
                return result.ToArray();
            }

            // Get the action map by the given Action Map name
            InputActionMap actionMap = _inputActionAsset.FindActionMap(_actionMapName);

            if (actionMap == null)
            {
                Debug.LogError("Action map not found: " + _actionMapName);
                return result.ToArray();
            }

            // Find the action in the action map by name
            InputAction action = null;
            try { action = actionMap.FindAction(_actionName, true); }
            catch (ArgumentException e) { action = null; }

            if (action != null)
            {
                // Filter the bindings using the control scheme's binding group
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    InputBinding binding = action.bindings[i];
                    if (binding.groups.Contains(controlScheme.Value.bindingGroup))
                    {
                        string tActionName = _actionName;
                        if (binding.isPartOfComposite) { tActionName = binding.name; }

                        // Get the key code from the binding
                        string keyName = binding.ToDisplayString();

                        var displayString = string.Empty;
                        var deviceLayoutName = default(string);
                        var controlPath = default(string);

                        displayString = action.GetBindingDisplayString(i, out deviceLayoutName, out controlPath, new InputBinding.DisplayStringOptions());

                        ActionBinding item = new ActionBinding(_schemeName, tActionName, deviceLayoutName, displayString);
                        result.Add(item);
                    }
                }
                return result.ToArray();
            }
            else
            {
                Debug.LogError("Action not found in the specified Action Map and Control Scheme: " + _actionName + ", " + _schemeName);
                return result.ToArray();
            }
        }
    }
}
