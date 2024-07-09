using System;
using Keyboard.Style;
using TMPro;
using UnityEngine;

namespace Keyboard.Key
{
    public class TextMeshKey: BaseKey
    {
        [SerializeField] protected TextMeshProUGUI keyTextMeshPro;
        [SerializeField] private bool ignoreShift;

        private string keyDefaultValue;
        
        public override string GetText => keyTextMeshPro.text;
        
        public override string SetText
        {
            set => keyTextMeshPro.text = value;
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