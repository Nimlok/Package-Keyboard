using UnityEngine;
using UnityEngine.UI;

namespace UI.Keyboard
{
    [RequireComponent(typeof(Image))]
    public class KeyboardImageStyleManager: MonoBehaviour
    {
        private Image keyImage;

        private void Awake()
        {
            keyImage = GetComponent<Image>();
        }

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