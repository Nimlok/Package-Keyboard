using System;
using TMPro;
using UI.Keyboard.Key;
using UI.Keyboard.Style;
using UnityEngine;
using UnityEngine.UI;

namespace Keyboard.Key
{
    [RequireComponent(typeof(Button))]
    public class TextMeshKey: BaseKey
    {
        [SerializeField] protected TextMeshProUGUI keyTextMeshPro;
        [SerializeField] private bool ignoreShift;
        
        public override string GetText => keyTextMeshPro.text;

        private Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        public override string SetText
        {
            set => keyTextMeshPro.text = value;
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

        public override void UpdateStyle(KeyboardStyleObject keyboardStyle)
        {
            if (ignoreStyle)
                return;
            UpdateTextMesh(keyboardStyle);
            UpdateBackgroundImage(keyboardStyle);
        }

        private void UpdateTextMesh(KeyboardStyleObject keyboardStyle)
        {
            if (keyTextMeshPro == null)
            {
                Debug.LogWarning($"Missing TextMesh from {gameObject.name}");
                return;
            }
            
            UpdateFont(keyboardStyle.keyTextStyle.font);
            UpdateTextColor(keyboardStyle.keyTextStyle.color);
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