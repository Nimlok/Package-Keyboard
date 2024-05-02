using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class KeyboardTextStyleManager: MonoBehaviour
    {
        private TextMeshProUGUI keyTextMesh;

        public TextMeshProUGUI GetTextMesh => keyTextMesh;

        private void Awake()
        {
            keyTextMesh = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateStyle(KeyboardTextStyle newStyle)
        {
            if (keyTextMesh == null)
            {
                Debug.LogWarning($"Missing Keyboard Image: {gameObject.name}");
                return;
            }
            
            UpdateColor(newStyle.color);
            UpdateFont(newStyle.font);
        }
        
        private void UpdateColor(Color newColor)
        {
            if (newColor == default)
                return;

            keyTextMesh.color = newColor;
        }

        private void UpdateFont(TMP_FontAsset newFont)
        {
            if (newFont == null)
                return;
            
            keyTextMesh.font = newFont;
        }
    }
}