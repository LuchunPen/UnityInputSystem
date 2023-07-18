using System;
using UnityEngine;
using UnityEngine.UI;
public class AutoSelector : MonoBehaviour
{
    [SerializeField] private Selectable _selector;

    private void OnEnable()
    {
        _selector.Select();
    }
}
