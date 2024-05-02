using UnityEngine;

namespace UI.Keyboard
{
    [RequireComponent(typeof(OnScreenKeyboard))]
    public class KeyboardStyleManager: MonoBehaviour
    {
        [SerializeField] private KeyboardStyle keyboardStyle;
        [Space]
        [SerializeField] private bool ignoreStyle;
        
        [Space]
        [SerializeField] private KeyboardImage keyboardBackground;
        [SerializeField] private KeyboardDisplay keyboardDisplay;

        private OnScreenKeyboard onScreenKeyboard;
        
        #region Unity Functions
        private void Awake()
        {
            onScreenKeyboard = GetComponent<OnScreenKeyboard>();
        }

        private void Start()
        {
            UpdateKeyboardStyle(keyboardStyle);
        }
        
        #endregion

        private void UpdateKeyboardStyle(KeyboardStyle keyboardStyle)
        {
            if (ignoreStyle)
                return;
            
            keyboardBackground.UpdateStyle(keyboardStyle.keyboardImageStyle);
            keyboardDisplay.UpdateStyle(keyboardStyle);
            
            foreach (var key in onScreenKeyboard.GetKeys)
            {
                key.UpdateStyle(keyboardStyle);
            }
        }
    }
}