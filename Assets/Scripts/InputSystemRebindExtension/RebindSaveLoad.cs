using UnityEngine;
using UnityEngine.InputSystem;

namespace RebindExtension
{
    public class RebindSaveLoad : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;

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
