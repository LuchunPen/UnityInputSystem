using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem.Samples.RebindUI;

using TMPro;

public class UI_KeyDisplayer : MonoBehaviour
{
    [SerializeField] private InputActionKeyGetter[] _keys;
    [SerializeField] private TextMeshProUGUI _text;

    public void Display()
    {
        List<KeyValuePair<string, StringBuilder>> kv = new List<KeyValuePair<string, StringBuilder>>();
        for (int i = 0; i < _keys.Length; i++)
        {
            InputActionKeyGetter.ActionBinding[] bindings = _keys[i].GetActionKeys();
            for (int j = 0; j < bindings.Length; j++)
            {
                InputActionKeyGetter.ActionBinding binding = bindings[j];
                StringBuilder value = GetActionKeys(kv, binding.ActionName);
                if (value == null)
                {
                    KeyValuePair<string, StringBuilder> pair = new KeyValuePair<string, StringBuilder>(binding.ActionName, new StringBuilder(binding.Key));
                    kv.Add(pair);
                }
                else
                {
                    value.Append(", " + binding.Key);
                }
            }
        }

        string total_text = string.Empty;
        for (int i = 0; i < kv.Count; i++)
        {
            total_text += kv[i].Key + ": " + kv[i].Value.ToString() + Environment.NewLine;
        }

        _text.text = total_text;
    }

    private StringBuilder GetActionKeys(List<KeyValuePair<string, StringBuilder>> list, string action)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Key == action) { return list[i].Value; }
        }

        return null;
    }
}
