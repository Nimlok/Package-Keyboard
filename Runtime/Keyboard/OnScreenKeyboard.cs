using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core;
using Keyboard.Key;
using Keyboard.UI;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Keyboard
{
    [ExecuteInEditMode]
    public class OnScreenKeyboard : MonoBehaviour
    {
        [SerializeField] private KeyboardDisplay keyboardDisplay;
        [SerializeField] private ABSAnimationComponent showTween;
        
        private bool shiftKey;
        private bool onShow;
        private TMP_InputField currentlySelectedInputField;
        private TMP_InputField.ContentType contentType;
        private List<BaseKey> keys = new List<BaseKey>();

        public List<BaseKey> GetKeys => keys;

        #region Unity Functions
        private void OnValidate()
        {
            keys ??= GetComponentsInChildren<BaseKey>().ToList();
        }
        
        private void Awake()
        {
            keys = GetComponentsInChildren<BaseKey>().ToList();
        }

        private void Start()
        {
            AssignTextKeyboard();
        }

        #endregion
        
        public void ShowKeyboard(TMP_InputField inputField)
        {
            AssignInput(inputField);
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
            
            showTween.DOPlayBackwards();
            onShow = false;
            ClearKeyboard();
        }
        
        public void ShiftKey()
        {
            shiftKey = !shiftKey;

            foreach (var key in keys)
            {
                key.ShiftKeyPressed(shiftKey);
            }
        }
    
        public void ReturnKey()
        {
            AddString("\n");
        }

        public void BackspaceKey()
        {
            var newDeletedCharacter = DeleteCharacter(currentlySelectedInputField.text);
            keyboardDisplay.ReplaceDisplayText(newDeletedCharacter);
            currentlySelectedInputField.text = newDeletedCharacter;
        }
        
        public BaseKey FindKey(string key)
        {
            return keys.Find(x => string.CompareOrdinal(x.GetText, key) == 0);
        }
        
        public void NextNavigation()
        {
            Navigate(Vector3.down);
        }

        public void BackNavigation()
        {
            Navigate(Vector3.up);
        }

        public void ResetKeysToDefault()
        {
            foreach (var key in keys)
            {
                key.ResetKeyToDefault();
            }
        }

        private void Navigate(Vector3 direction)
        {
            if (currentlySelectedInputField == null)
                return;
            
            if (NextObjectSelected(direction))
                return;
        
            HideKeyboard();
        }
    
        private bool NextObjectSelected(Vector3 direction)
        {
            var nextObject = direction == Vector3.down ? currentlySelectedInputField.FindSelectableOnDown() 
                : currentlySelectedInputField.FindSelectableOnUp();
        
        
            if (nextObject == null)
            {
                return false;
            }
        
            if (!IsInputField(nextObject))
            {
                HideKeyboard();
                return true;
            }
            
            nextObject.Select();
            return true;
        }
    
        private bool IsInputField(Selectable nextObject)
        {
            return nextObject.GetType().IsCastableTo(typeof(TMP_InputField));
        }
        
        private void ClearKeyboard()
        {
            keyboardDisplay.ClearText();
        }
        
        private void AssignTextKeyboard()
        {
            foreach (var baseKey in keys)
            {
                if (!baseKey.GetType().IsCastableTo(typeof(TextMeshKey))) 
                    continue;
                
                var textKey = (TextMeshKey)baseKey;
                textKey.AddListener(KeyEntered);
            }
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
            
            showTween.DOPlayForward();
            onShow = true;
        }
        
        private void AssignInput(TMP_InputField inputField)
        {
            currentlySelectedInputField = inputField;
            keyboardDisplay.ReplaceDisplayText(currentlySelectedInputField.text);
            keyboardDisplay.UpdatePlaceholder(inputField.placeholder.GetComponent<TMP_Text>().text);
        }

        private void KeyEntered(TextMeshKey character)
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
            currentlySelectedInputField.text += newString;
        }
        
        private static string DeleteCharacter(string currentText)
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
