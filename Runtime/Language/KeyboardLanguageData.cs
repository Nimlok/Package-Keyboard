using System;
using UnityEngine;

namespace Keyboard.Language
{
    [Serializable]
    public struct Languagekeys
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