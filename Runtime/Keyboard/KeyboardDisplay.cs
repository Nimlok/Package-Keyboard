using System;
using Nimlok.Keyboard.Style;
using UnityEngine;

namespace Nimlok.Keyboard.UI
{
    public class KeyboardDisplay: MonoBehaviour
    {
        [SerializeField] private TextKeyStyleManager displayTextKeyStyleManagerMesh;
        [SerializeField] private KeyboardPlaceholder placeholder;
        [SerializeField] private IconKeyStyleManager backgroundImageStyleManager;

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
        
        #if UNITY_EDITOR
        private void Update()
        {
            if (!Input.anyKeyDown)
                return;

            if (Input.GetKey(KeyCode.Backspace))
            {
                var currentString = displayTextKeyStyleManagerMesh.GetTextMesh.text;
                currentString = currentString.Substring(0, currentString.Length - 1);
                ReplaceDisplayText(currentString);
                return;
            }
            
            AddToDisplayText(Input.inputString);
        }
        #endif
    }
}