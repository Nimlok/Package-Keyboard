using System;
using Nimlok.Keyboard.Style;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nimlok.Keyboard.Key
{
    public abstract class BaseKey : MonoBehaviour
    {
        [SerializeField] private TMP_InputField.ContentType contentType;
        [SerializeField] protected IconKeyStyleManager backgroundImageStyleManagerStyle;
        [SerializeField] protected Button button;

        public TMP_InputField.ContentType GetContentType => contentType;

        public virtual string GetText => null;

        public abstract void SetText(string text);

        public abstract void SeTextDefault(string text);
        
        public abstract void ShiftKeyPressed(bool shifted);

        public abstract void UpdateStyle(KeyboardStyleObject keyboardStyle);
        
        public abstract void ResetKeyToDefault();

        public abstract void FullResetKey();
        
        
        protected void UpdateBackgroundImage(BackgroundStyle keyboardStyle)
        {
            if (backgroundImageStyleManagerStyle == null)
            {
                Debug.LogError($"Missing Image from {name}");
                return;
            }

            button.image.color = keyboardStyle.color;
            button.image.sprite = keyboardStyle.imageOn;
            button.spriteState = new SpriteState()
            {
                pressedSprite = keyboardStyle.imageOff
            };
        }
    }
}