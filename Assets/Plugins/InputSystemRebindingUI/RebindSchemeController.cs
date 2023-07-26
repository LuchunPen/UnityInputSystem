using System;
using UnityEngine;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class RebindSchemeController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actions;
        [SerializeField] private string _controlScheme;

        public void ResetScheme()
        {
            RebindHelper.ResetBindings(_actions, _controlScheme);
        }

        public void ApplyBindings()
        {
            RebindHelper.SaveBindings(_actions);
        }

        public void DisardBindings()
        {
            bool success = RebindHelper.LoadBindings(_actions);
            if (!success) { ResetScheme(); }
        }
    }
}
