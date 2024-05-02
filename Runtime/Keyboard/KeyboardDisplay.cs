using UnityEngine;

namespace UI.Keyboard
{
    public class KeyboardDisplay: MonoBehaviour
    {
        [SerializeField] private KeyboardText displayTextMesh;
        [SerializeField] private KeyboardPlaceholder placeholder;
        [SerializeField] private KeyboardImage backgroundImage;

        public void AddToDisplayText(string character)
        {
            displayTextMesh.GetTextMesh.text += character;
        }

        public void ReplaceDisplayText(string newText)
        {
            displayTextMesh.GetTextMesh.text = newText;
        }
        
        public void ClearText()
        {
            displayTextMesh.GetTextMesh.text = string.Empty;
        }

        public void DisplayError(string errorMessage)
        {
            ClearText();
            placeholder.ShowErrorMessage(errorMessage);
        }

        public void UpdatePlaceholder(string message)
        {
            placeholder.UpdatePlaceholder(message);
        }
        
        public void UpdateStyle(KeyboardStyle keyboardStyle)
        {
            if (backgroundImage == null)
            {
                Debug.LogWarning($"Missing Image: {name}");
                return;
            }
            
            backgroundImage.UpdateStyle(keyboardStyle.displayImageStyle);
            displayTextMesh.UpdateStyle(keyboardStyle.displayTextStyle);
            placeholder.UpdatePlaceholderStyle(keyboardStyle.placeholderTextStyle);
        }
        
        public string DeleteCharacter(string currentText)
        {
            if (string.IsNullOrEmpty(currentText))
                return null;
            
            currentText = currentText.Substring(0, currentText.Length - 1);
            displayTextMesh.GetTextMesh.text = currentText;
            return currentText;
        }
    }
}