using System;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class ResetDeviceBindings : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        [SerializeField] private string _controlScheme;

        public void ResetBindings()
        {
            foreach (InputActionMap map in _inputActions.actionMaps)
            {
                foreach(InputAction action in map.actions)
                {
                    action.RemoveBindingOverride(InputBinding.MaskByGroup(_controlScheme));
                }
            }
        }
    }
}
