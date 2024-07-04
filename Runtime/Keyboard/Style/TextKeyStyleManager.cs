using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    public class TextKeyStyleManager: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyTextMesh;

        public TextMeshProUGUI GetTextMesh => keyTextMesh;

        public void UpdateStyle(TextKeyStyle newKeyStyle)
        {
            if (keyTextMesh == null)
            {
                Debug.LogWarning($"Missing Keyboard Image: {gameObject.name}");
                return;
            }
            
            UpdateColor(newKeyStyle.color);
            UpdateFont(newKeyStyle.font);
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