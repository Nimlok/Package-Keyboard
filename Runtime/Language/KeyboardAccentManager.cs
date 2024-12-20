using System.Collections.Generic;
using Nimlok.Keyboard;
using Nimlok.Keyboard.Language;
using UnityEngine;

[RequireComponent(typeof(OnScreenKeyboard))]
public class KeyboardAccentManager : MonoBehaviour
{
    [SerializeField] private List<Languagekeys> accentKeys = new List<Languagekeys>();

    private OnScreenKeyboard onScreenKeyboard;
    private bool active;
    
    
    private void Start()
    {
        onScreenKeyboard = GetComponent<OnScreenKeyboard>();
    }

    public void ShowAccentKeys()
    {
        if (accentKeys.Count <= 0)
            return;
        
        if (active)
        {
            active = false;
            onScreenKeyboard.ResetKeysToDefault();
            return;
        }
        
        active = true;
        foreach (var key in onScreenKeyboard.GetKeys)
        {
            var accentKey = accentKeys.Find(x => string.CompareOrdinal(x.key, key.GetText) == 0);
            if (accentKey == null)
            {
                key.SetText(string.Empty);
                continue;
            }
            
            key.SetText(accentKey.languageKey);
        }
    }
}
