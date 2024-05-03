using UnityEngine;
using UnityEngine.UI;

namespace UI.Keyboard
{
    public class KeyboardImageStyleManager: MonoBehaviour
    {
        [SerializeField] private Image keyImage;
        
        public void UpdateStyle(KeyboardImageStyle newStyle)
        {
            if (keyImage == null)
            {
                Debug.LogWarning($"Missing Keyboard Image: {gameObject.name}");
                return;
            }
            
            UpdateColor(newStyle.color);
            UpdateBackgroundSprite(newStyle.sprite);
        }
        
        private void UpdateColor(Color newColor)
        {
            if (newColor == default )
                return;

            keyImage.color = newColor;
        }

        private void UpdateBackgroundSprite(Sprite newSprite)
        {
            if (newSprite == null)
                return;

            keyImage.sprite = newSprite;
        }
    }
}