using UnityEngine;
using UnityEngine.UI;

namespace UI.Keyboard
{
    public class IconKeyStyleManager: MonoBehaviour
    {
        [SerializeField] private Image keyImage;
        
        public void UpdateStyle(IconKeyStyle newStyle)
        {
            if (keyImage == null)
            {
                Debug.LogWarning($"Missing Keyboard Image: {gameObject.name}");
                return;
            }
            
            UpdateColor(newStyle.color);
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