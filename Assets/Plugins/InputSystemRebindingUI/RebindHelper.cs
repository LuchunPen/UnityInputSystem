using System;
using UnityEngine;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public static class RebindHelper
    {
        public static bool SaveBindings(InputActionAsset actions)
        {
            var rebinds = actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("rebinds", rebinds);
            return true;
        }

        public static bool LoadBindings(InputActionAsset actions)
        {
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds)) 
            {
                actions.LoadBindingOverridesFromJson(rebinds);
                return true;
            }
            return false;
        }

        public static void ResetBindings(InputActionAsset actions,  string scheme)
        {
            if (string.IsNullOrEmpty(scheme)) { return; }

            foreach (InputActionMap map in actions.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    action.RemoveBindingOverride(InputBinding.MaskByGroup(scheme));
                }
            }
        }
    }
}
