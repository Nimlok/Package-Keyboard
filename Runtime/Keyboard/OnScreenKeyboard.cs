using System;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    public class OnScreenKeyboard : MonoBehaviour
    {
        [SerializeField] private KeyboardDisplay keyboardDisplay;
        [SerializeField] private ABSAnimationComponent showTween;
        
        private bool shiftKey;
        private bool onShow;
        private TMP_InputField currentlySelectedInputField;
        private KeyboardKey[] keys;
        private TMP_InputField.ContentType contentType;

        public KeyboardKey[] GetKeys => keys;
        
        public static Action<KeyboardKey> onKeyPressed;
        public static Action onEnterPressed;

        #region Unity Functions
        private void OnEnable()
        {
            onKeyPressed += KeyEntered;
        }

        private void OnDisable()
        {
            onKeyPressed -= KeyEntered;
        }

        private void Awake()
        {
            keys = GetComponentsInChildren<KeyboardKey>();
        }
        

        #endregion
        
        public void ShowKeyboard(TMP_InputField inputField)
        {
            if (inputField == null)
            {
                Debug.LogError($"Keyboard missing inputfield");
                return;
            }

            contentType = inputField.contentType;
            SelectedInput(inputField);
            PlayKeyboardTransition();
        }
        
        public void HideKeyboard()
        {
            if (showTween == null)
            {
                Debug.LogError("Missing Show Keyboard Tween");
                return;
            }

            if (!onShow) 
                return;
            
            showTween.DOPlayForward();
            onShow = false;
            ClearKeyboard();
        }
        
        public void ShiftKey()
        {
            shiftKey = !shiftKey;

            foreach (var key in keys)
            {
                key.SetKeyUpper(shiftKey);
            }
        }
    
        public void ReturnKey()
        {
            AddString("\n");
        }

        public void GoKey()
        {
            onEnterPressed?.Invoke();
        }

        public void DeleteKey()
        {
            var newDeletedCharacter = DeleteCharacter(currentlySelectedInputField.text);
            keyboardDisplay.ReplaceDisplayText(newDeletedCharacter);
            currentlySelectedInputField.text = newDeletedCharacter;
        }
        
        public void ClearKeyboard()
        {
            keyboardDisplay.ClearText();
        }
        
        private void PlayKeyboardTransition()
        {
            if (showTween == null)
            {
                Debug.LogError("Missing Show Keyboard Tween");
                return;
            }

            if (onShow) 
                return;
            
            showTween.DOPlayBackwards();
            onShow = true;
        }
        
        private void SelectedInput(TMP_InputField inputField)
        {
            if (inputField == null)
            {
                Debug.LogError($"{name} Missing Inputfield");
                return;
            }
            
            currentlySelectedInputField = inputField;
            keyboardDisplay.ReplaceDisplayText(currentlySelectedInputField.text);
            keyboardDisplay.UpdatePlaceholder(inputField.placeholder.GetComponent<TMP_Text>().text);
        }

        private void KeyEntered(KeyboardKey character)
        {
            if (!CharacterValid(character.GetContentType))
            {
                return;
            }
            
            AddString(character.GetText);
            keyboardDisplay.AddToDisplayText(character.GetText);
        }
        
        private void AddString(string newString)
        {
            if (shiftKey)
            {
                newString = newString.ToUpper();
            }
            
            currentlySelectedInputField.text += newString;
        }
        
        private string DeleteCharacter(string currentText)
        {
            if (string.IsNullOrEmpty(currentText))
                return null;
            
            currentText = currentText.Substring(0, currentText.Length - 1);
            return currentText;
        }

        private bool CharacterValid(TMP_InputField.ContentType newContentType)
        {
            if (newContentType == contentType)
            {
                return true;
            }

            switch (contentType)
            {
                case TMP_InputField.ContentType.Standard:
                    return true;
                case TMP_InputField.ContentType.Autocorrected:
                    break;
                case TMP_InputField.ContentType.IntegerNumber:
                    return newContentType == TMP_InputField.ContentType.IntegerNumber;
                case TMP_InputField.ContentType.DecimalNumber:
                    return false;
                case TMP_InputField.ContentType.Alphanumeric:
                    return newContentType == TMP_InputField.ContentType.Alphanumeric;
                case TMP_InputField.ContentType.Name:
                    return newContentType == TMP_InputField.ContentType.Alphanumeric;
                case TMP_InputField.ContentType.EmailAddress:
                    return true;
                case TMP_InputField.ContentType.Password:
                    return true;
                case TMP_InputField.ContentType.Pin:
                    return newContentType == TMP_InputField.ContentType.Alphanumeric;
                case TMP_InputField.ContentType.Custom:
                    return true;
                default:
                    return true;
            }

            return false;
        }
    }
}
