using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ErrorMessaging
{
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldInvalidDisplay: BaseInvalidDisplay
    {
        [SerializeField] private Image backgroundImage;
        
        private TMP_InputField inputField;
        private TMP_Text inputFieldPlaceholderText;
        private Color defaultBackgroundImage;

        private void OnDisable()
        {
            ResetMessage();
        }

        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            inputFieldPlaceholderText = inputField.placeholder.GetComponent<TMP_Text>();
            DefaultMessage = inputFieldPlaceholderText.text;
            DefaultColor = inputFieldPlaceholderText.color;
            if (backgroundImage != null)
            {
                defaultBackgroundImage = backgroundImage.color;
            }
        }

        public override void TriggerInvalidMessage()
        {
            inputField.text = string.Empty;
            AdjustText(message, color);
            AdjustBackground(color);
            if(tween != null) 
                tween.DOPlay();
        }

        public override void ResetMessage()
        {
            inputField.text = string.Empty;
            AdjustText(DefaultMessage, DefaultColor);
            AdjustBackground(defaultBackgroundImage);
        }
        
        private void AdjustText(string newText, Color newColor)
        {
            inputFieldPlaceholderText.text = newText;
            inputFieldPlaceholderText.color = newColor;
        }

        private void AdjustBackground(Color newColor)
        {
            if (backgroundImage != null)
                backgroundImage.color = newColor;
        }
    }
}