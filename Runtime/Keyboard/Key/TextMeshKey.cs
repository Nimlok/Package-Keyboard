using System;
using Nimlok.Keyboard.Style;
using TMPro;
using UnityEngine;

namespace Nimlok.Keyboard.Key
{
    public class TextMeshKey: BaseKey
    {
        [SerializeField] protected TextMeshProUGUI keyTextMeshPro;
        [SerializeField] private bool ignoreShift;

        private string keyDefaultValue;
        
        public override string GetText => keyTextMeshPro.text;

        private void OnValidate()
        {
            if(!string.IsNullOrEmpty(keyDefaultValue))
                return;
            
            keyDefaultValue = keyTextMeshPro.text;
        }

        private void Awake()
        {
            keyDefaultValue = keyTextMeshPro.text;
        }

        public void AddListener(Action<TextMeshKey> onKeyPressed)
        {
            if (button == null)
                return;
            
            button.onClick.AddListener(() => onKeyPressed?.Invoke(this));
        }

        public override void SetText(string text)
        {
            keyTextMeshPro.SetText(text);
        }

        public override void SeTextDefault(string text)
        {
            keyTextMeshPro.SetText(text);
            keyDefaultValue = text;
        }

        public override void ShiftKeyPressed(bool shifted)
        {
            if (ignoreShift)
                return;
            
            keyTextMeshPro.text = shifted ? keyTextMeshPro.text.ToUpper() : keyTextMeshPro.text.ToLower();
        }

        public override void ResetKeyToDefault()
        {
            keyTextMeshPro.text = keyDefaultValue;
        }

        public override void UpdateStyle(KeyboardStyleObject keyboardStyle)
        {
            UpdateTextMesh(keyboardStyle);
            UpdateBackgroundImage(keyboardStyle.keyBackgroundStyle);
        }

        private void UpdateTextMesh(KeyboardStyleObject keyboardStyle)
        {
            if (keyTextMeshPro == null)
            {
                Debug.LogWarning($"Missing TextMesh from {gameObject.name}");
                return;
            }
            
            UpdateFont(keyboardStyle.keyTextKeyStyle.font);
            UpdateTextColor(keyboardStyle.keyTextKeyStyle.color);
        }

        private void UpdateFont(TMP_FontAsset font)
        {
            if(font == null)
                return;

            keyTextMeshPro.font = font;
        }

        private void UpdateTextColor(Color newTextColor)
        {
            if (newTextColor == default)
                return;

            keyTextMeshPro.color = newTextColor;
        }
    }
}