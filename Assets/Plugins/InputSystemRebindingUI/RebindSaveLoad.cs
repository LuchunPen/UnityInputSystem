using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class RebindSaveLoad : MonoBehaviour
    {
        public InputActionAsset actions;

        public void SaveBindings()
        {
            var rebinds = actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("rebinds", rebinds);
        }

        public void LoadBindings()
        {
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds)) {
                actions.LoadBindingOverridesFromJson(rebinds);
            }
        }
    }
}
