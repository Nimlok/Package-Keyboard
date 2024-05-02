using ErrorMessaging;
using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class KeyboardPlaceholder: MonoBehaviour
    {
        [SerializeField] private string defaultString;
        [SerializeField] private BaseInvalidDisplay invalidDisplay;
        [SerializeField] private KeyboardText placeholderTextMesh;
        
        private TextMeshProUGUI textMesh;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            textMesh.text = defaultString;
        }

        public void UpdatePlaceholder(string newString)
        {
            textMesh.text = newString;
        }

        public void ShowErrorMessage(string errorMessage)
        {
            invalidDisplay.SetMessage = errorMessage;
            invalidDisplay.TriggerInvalidMessage();
        }

        public void UpdatePlaceholderStyle(KeyboardTextStyle placeholderStyle)
        {
            if (placeholderTextMesh == null)
            {
                Debug.LogWarning($"Missing BackgroundImage: {gameObject.name}");
                return;
            }
            
            placeholderTextMesh.UpdateStyle(placeholderStyle);
        }
        
        public void ClearPlaceholder()
        {
            textMesh.text = string.Empty;
        }
    }
}