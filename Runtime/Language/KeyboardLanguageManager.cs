using Sirenix.OdinInspector;
using UI.Keyboard;
using UnityEngine;

namespace Keyboard.Language
{
    [RequireComponent(typeof(OnScreenKeyboard))]
    public class KeyboardLanguageManager : MonoBehaviour
    {
        [SerializeField, OnValueChanged("SetKeys")] private KeyboardLanguageObject keyboardLanguageData;

        private OnScreenKeyboard onScreenKeyboard;

        private void Awake()
        {
            onScreenKeyboard = GetComponent<OnScreenKeyboard>();
        }

        private void Start()
        {
            SetKeys();
        }

        private void SetKeys()
        {
            if (keyboardLanguageData == null)
                return;
            
            foreach (var languageKey in keyboardLanguageData.LanguageKeys)
            {
                var BaseKey = onScreenKeyboard.FindKey(languageKey.key);
                if (BaseKey == null)
                    continue;

                BaseKey.SetText = languageKey.languageKey;
            }
        }
    }
}

