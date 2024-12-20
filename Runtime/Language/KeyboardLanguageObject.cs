using System;
using UnityEngine;

namespace Nimlok.Keyboard.Language
{
    [Serializable]
    public class Languagekeys
    {
        public string key;
        public string languageKey;
    }
    
    [CreateAssetMenu(menuName = "OnScreenKeyboard/Language")]
    public class KeyboardLanguageObject: ScriptableObject
    {
        public Languagekeys[] LanguageKeys;
    }
}