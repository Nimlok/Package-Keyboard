using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    public class KeyboardTextStyleManager: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyTextMesh;

        public TextMeshProUGUI GetTextMesh => keyTextMesh;

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