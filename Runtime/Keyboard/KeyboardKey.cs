using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    public class KeyboardKey: MonoBehaviour
    {
        [SerializeField] private TMP_InputField.ContentType contentType;
        [SerializeField] private TextMeshProUGUI keyTextMeshPro;
        [SerializeField] private KeyboardImage backgroundImageStyle;
        
        [Space]
        [SerializeField] private bool ignoreStyle;

        public TMP_InputField.ContentType GetContentType => contentType;
        public string GetText => keyTextMeshPro.text;
        
        public void KeyPressed()
        {
            if (keyTextMeshPro == null)
            {
                Debug.LogError($"Missing textmesh from Key {name}");
                return;
            }
            
            OnScreenKeyboard.onKeyPressed?.Invoke(this);
        }

        public void SetKeyUpper(bool upper)
        {
            keyTextMeshPro.text = upper ? keyTextMeshPro.text.ToUpper() : keyTextMeshPro.text.ToLower();
        }

        public void UpdateStyle(KeyboardStyle keyboardStyle)
        {
            if (ignoreStyle)
                return;
            UpdateTextMesh(keyboardStyle);
            UpdateImage(keyboardStyle);
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

        private void UpdateImage(KeyboardStyle keyboardStyle)
        {
            if (backgroundImageStyle == null)
            {
                Debug.LogError($"Missing Image from {name}");
                return;
            }
            
            backgroundImageStyle.UpdateStyle(keyboardStyle.keyImageStyle);
        }
    }
}