using TMPro;
using UI.Keyboard.Style;
using UnityEngine;

namespace UI.Keyboard.Key
{
    public abstract class BaseKey: MonoBehaviour
    {
        [SerializeField] protected bool ignoreStyle;
        [Space]
        [SerializeField] private TMP_InputField.ContentType contentType;
        [SerializeField] protected KeyboardImageStyleManager backgroundImageStyleManagerStyle;

        public TMP_InputField.ContentType GetContentType => contentType;

        public virtual string GetText => null;

        public virtual string SetText
        {
            set
            {
                return;
            }
        }

        public abstract void KeyPressed();
        
        public abstract void ShiftKeyPressed(bool shifted);

        public abstract void UpdateStyle(KeyboardStyleObject keyboardStyle);
        
        protected void UpdateBackgroundImage(KeyboardStyleObject keyboardStyle)
        {
            if (backgroundImageStyleManagerStyle == null)
            {
                Debug.LogError($"Missing Image from {name}");
                return;
            }
            
            backgroundImageStyleManagerStyle.UpdateStyle(keyboardStyle.keyBackgroundStyle);
        }
    }
}