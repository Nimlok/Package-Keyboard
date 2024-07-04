using Keyboard.UI;
using UnityEngine;

namespace Keyboard.Style
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(OnScreenKeyboard))]
    public class KeyboardStyleManager: MonoBehaviour
    {
        [SerializeField] private KeyboardStyleObject keyboardStyle;
        [Space]
        [SerializeField] private IconKeyStyleManager keyboardBackground;
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
            if (keyboardStyle == null)
                return;
            
            keyboardBackground.UpdateStyle(keyboardStyle.backgroundStyle);
            keyboardDisplay.UpdateStyle(keyboardStyle);

            if (onScreenKeyboard.GetKeys == null)
                return;
            
            foreach (var key in onScreenKeyboard.GetKeys)
            {
                key.UpdateStyle(keyboardStyle);
            }
        }
    }
}