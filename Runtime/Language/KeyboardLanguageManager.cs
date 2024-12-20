using Sirenix.OdinInspector;
using UnityEngine;

namespace Nimlok.Keyboard.Language
{
    [RequireComponent(typeof(OnScreenKeyboard))]
    public class KeyboardLanguageManager : MonoBehaviour
    {
        [SerializeField, OnValueChanged("SetEditorKeys")] private KeyboardLanguageObject keyboardLanguageData;
        [SerializeField] private bool onStart;
        
        private OnScreenKeyboard onScreenKeyboard;
        private bool active;

        #if UNITY_EDITOR
        private void SetEditorKeys()
        {
            if (onScreenKeyboard == null)
            {
                onScreenKeyboard = GetComponent<OnScreenKeyboard>();
            }
            
            if (keyboardLanguageData == null)
            {
                onScreenKeyboard.ResetKeysToDefault();
                return;
            }
            
            SetKeys();
        }
        
        #endif
        
        private void Awake()
        {
            onScreenKeyboard = GetComponent<OnScreenKeyboard>();
        }

        private void Start()
        {
            if(onStart) 
                SetKeys();
        }

        private void SetKeys()
        {
            if (keyboardLanguageData == null || onScreenKeyboard.GetKeys is not { Count: > 0 } )
                return;

            foreach (var languageKey in keyboardLanguageData.LanguageKeys)
            {
                var key = onScreenKeyboard.GetKeys.Find(x => x.GetText == languageKey.key);
                if (key == null)
                    continue;

                key.SeTextDefault(languageKey.languageKey);
            }
        }
    }
}

