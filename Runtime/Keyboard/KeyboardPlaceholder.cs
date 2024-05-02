using ErrorMessaging;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Keyboard
{
    public class KeyboardPlaceholder: MonoBehaviour
    {
        [SerializeField] private string defaultString;
        [SerializeField] private BaseInvalidDisplay invalidDisplay;
        [FormerlySerializedAs("placeholderTextMesh")] [SerializeField] private KeyboardTextStyleManager placeholderTextStyleManagerMesh;
        
        #region Unity Functions
        private void Awake()
        {
            UpdatePlaceholder(defaultString);
        }
        #endregion

        public void UpdatePlaceholder(string newString)
        {
            if (placeholderTextStyleManagerMesh == null)
            {
                Debug.LogWarning($"Missing BackgroundImage: {gameObject.name}");
                return;
            }
            
            placeholderTextStyleManagerMesh.GetTextMesh.text = newString;
        }

        public void ShowErrorMessage(string errorMessage)
        {
            invalidDisplay.SetMessage = errorMessage;
            invalidDisplay.TriggerInvalidMessage();
        }

        public void UpdatePlaceholderStyle(KeyboardTextStyle placeholderStyle)
        {
            if (placeholderTextStyleManagerMesh == null)
            {
                Debug.LogWarning($"Missing BackgroundImage: {gameObject.name}");
                return;
            }
            
            placeholderTextStyleManagerMesh.UpdateStyle(placeholderStyle);
        }
        
        public void ClearPlaceholder()
        {
            placeholderTextStyleManagerMesh.GetTextMesh.text = string.Empty;
        }
    }
}