using ErrorMessaging;
using Keyboard.Style;
using UnityEngine;

namespace Keyboard.UI
{
    public class KeyboardPlaceholder: MonoBehaviour
    {
        [SerializeField] private string defaultString;
        [SerializeField] private BaseInvalidDisplay invalidDisplay;
        [SerializeField] private TextKeyStyleManager placeholderTextKeyStyleManagerMesh;
        [SerializeField] private IconKeyStyleManager backgroundImageManager;
        
        #region Unity Functions
        private void Awake()
        {
            UpdatePlaceholder(defaultString);
        }
        #endregion

        public void UpdatePlaceholder(string newString)
        {
            if (placeholderTextKeyStyleManagerMesh == null)
            {
                Debug.LogWarning($"Missing BackgroundImage: {gameObject.name}");
                return;
            }
            
            placeholderTextKeyStyleManagerMesh.GetTextMesh.text = newString;
        }

        public void ShowErrorMessage(string errorMessage)
        {
            invalidDisplay.SetMessage = errorMessage;
            invalidDisplay.TriggerInvalidMessage();
        }

        public void UpdateTextStyle(TextKeyStyle placeholderKeyStyle)
        {
            if (placeholderTextKeyStyleManagerMesh == null)
            {
                Debug.LogWarning($"Missing TextMesh: {gameObject.name}");
                return;
            }
            
            placeholderTextKeyStyleManagerMesh.UpdateStyle(placeholderKeyStyle);
           
        }

        public void UpdateBackgroundStyle(IconKeyStyle placeholderBackgroundStyle)
        {
            if (backgroundImageManager == null)
            {
                Debug.LogWarning($"Missing BackgroundImage: {gameObject.name}");
                return;
            }
            
            backgroundImageManager.UpdateStyle(placeholderBackgroundStyle);
        }
    }
}