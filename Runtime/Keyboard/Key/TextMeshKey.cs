using TMPro;
using UI.Keyboard;
using UI.Keyboard.Key;
using UnityEngine;

namespace Keyboard.Key
{
    public class TextMeshKey: BaseKey
    {
        [SerializeField] protected TextMeshProUGUI keyTextMeshPro;
        
        public string GetText => keyTextMeshPro.text;
        
        public override void KeyPressed()
        {
            if (keyTextMeshPro == null)
            {
                Debug.LogError($"Missing textmesh from Key {name}");
                return;
            }
            
            OnScreenKeyboard.onKeyPressed?.Invoke(this);
        }

        public override void ShiftKeyPressed(bool shifted)
        {
            keyTextMeshPro.text = shifted ? keyTextMeshPro.text.ToUpper() : keyTextMeshPro.text.ToLower();
        }

        public override void UpdateStyle(KeyboardStyle keyboardStyle)
        {
            if (ignoreStyle)
                return;
            UpdateTextMesh(keyboardStyle);
            UpdateBackgroundImage(keyboardStyle);
        }

        private void UpdateTextMesh(KeyboardStyle keyboardStyle)
        {
            if (keyTextMeshPro == null)
            {
                Debug.LogError($"Missing TextMesh from {gameObject.name}");
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