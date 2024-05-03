using UI.Keyboard.Style;
using UnityEngine;

namespace UI.Keyboard
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(OnScreenKeyboard))]
    public class KeyboardStyleManager: MonoBehaviour
    {
        [SerializeField] private KeyboardStyleObject keyboardStyle;
        
        [Space]
        [SerializeField] private bool ignoreStyle;
        
        [Space]
        [SerializeField] private KeyboardImageStyleManager keyboardBackground;
        [SerializeField] private KeyboardDisplay keyboardDisplay;

        private OnScreenKeyboard onScreenKeyboard;
        private bool editorEventAdded;
        
        #region Unity Functions

        private void OnValidate()
        {
            if (keyboardStyle == null)
            {
                editorEventAdded = false;
                return;
            }
            
            onScreenKeyboard ??= GetComponent<OnScreenKeyboard>();
            
            if(editorEventAdded)
                return;
            keyboardStyle.OnObjectChange += () => UpdateKeyboardStyle(keyboardStyle);
            UpdateKeyboardStyle(keyboardStyle);
        }
        
        
        private void Awake()
        {
            onScreenKeyboard = GetComponent<OnScreenKeyboard>();
        }

        private void Start()
        {
            UpdateKeyboardStyle(keyboardStyle);
        }
        
        #endregion

        private void UpdateKeyboardStyle(KeyboardStyleObject keyboardStyle)
        {
            if (ignoreStyle)
                return;
            
            keyboardBackground.UpdateStyle(keyboardStyle.keyboardBackgroundStyle);
            keyboardDisplay.UpdateStyle(keyboardStyle);
            
            foreach (var key in onScreenKeyboard.GetKeys)
            {
                key.UpdateStyle(keyboardStyle);
            }
        }
    }
}