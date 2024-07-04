using UI.Keyboard.Style;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Keyboard
{
    public class KeyboardDisplay: MonoBehaviour
    {
        [FormerlySerializedAs("displayTextStyleManagerMesh")] [FormerlySerializedAs("displayTextMesh")] [SerializeField] private TextKeyStyleManager displayTextKeyStyleManagerMesh;
        [SerializeField] private KeyboardPlaceholder placeholder;
        [FormerlySerializedAs("backgroundImage")] [SerializeField] private IconKeyStyleManager backgroundImageStyleManager;

        public void AddToDisplayText(string character)
        {
            displayTextKeyStyleManagerMesh.GetTextMesh.text += character;
        }

        public void ReplaceDisplayText(string newText)
        {
            displayTextKeyStyleManagerMesh.GetTextMesh.text = newText;
        }
        
        public void ClearText()
        {
            displayTextKeyStyleManagerMesh.GetTextMesh.text = string.Empty;
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
            displayTextKeyStyleManagerMesh.UpdateStyle(keyboardStyle.displayTextKeyStyle);
            placeholder.UpdateTextStyle(keyboardStyle.placeholderTextKeyStyle);
            placeholder.UpdateBackgroundStyle(keyboardStyle.placeHolderBackgroundStyle);
        }
        
        public string DeleteCharacter(string currentText)
        {
            if (string.IsNullOrEmpty(currentText))
                return null;
            
            currentText = currentText.Substring(0, currentText.Length - 1);
            displayTextKeyStyleManagerMesh.GetTextMesh.text = currentText;
            return currentText;
        }
    }
}