using UI.Keyboard.Style;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Keyboard
{
    public class KeyboardDisplay: MonoBehaviour
    {
        [FormerlySerializedAs("displayTextMesh")] [SerializeField] private KeyboardTextStyleManager displayTextStyleManagerMesh;
        [SerializeField] private KeyboardPlaceholder placeholder;
        [FormerlySerializedAs("backgroundImage")] [SerializeField] private KeyboardImageStyleManager backgroundImageStyleManager;

        public void AddToDisplayText(string character)
        {
            displayTextStyleManagerMesh.GetTextMesh.text += character;
        }

        public void ReplaceDisplayText(string newText)
        {
            displayTextStyleManagerMesh.GetTextMesh.text = newText;
        }
        
        public void ClearText()
        {
            displayTextStyleManagerMesh.GetTextMesh.text = string.Empty;
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
        
        public void UpdateStyle(KeyboardStyleObject keyboardStyle)
        {
            if (backgroundImageStyleManager == null)
            {
                Debug.LogWarning($"Missing Image: {name}");
                return;
            }
            
            backgroundImageStyleManager.UpdateStyle(keyboardStyle.displayBackgroundStyle);
            displayTextStyleManagerMesh.UpdateStyle(keyboardStyle.displayTextStyle);
            placeholder.UpdatePlaceholderStyle(keyboardStyle.placeholderTextStyle);
        }
        
        public string DeleteCharacter(string currentText)
        {
            if (string.IsNullOrEmpty(currentText))
                return null;
            
            currentText = currentText.Substring(0, currentText.Length - 1);
            displayTextStyleManagerMesh.GetTextMesh.text = currentText;
            return currentText;
        }
    }
}