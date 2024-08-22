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
            if (!onStart)
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

        public void SetKeys()
        {
            if (keyboardLanguageData == null)
                return;

            if (active)
            {
                active = false;
                onScreenKeyboard.ResetKeysToDefault();
            }
            else
            {
                active = true;
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
}

