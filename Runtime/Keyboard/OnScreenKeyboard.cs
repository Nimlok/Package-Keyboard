using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core;
using Nimlok.Keyboard.Key;
using Nimlok.Keyboard.UI;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nimlok.Keyboard
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

        public List<BaseKey> GetKeys
        {
            get
            {
                if (keys.Count == 0)
                {
                    keys = GetComponentsInChildren<BaseKey>().ToList();
                }
                
                return keys;
            }
        }


        private int currentPosition;
        private int currentCaretPosition;

        #region Unity Functions
        private void OnValidate()
        {
            keys ??= GetComponentsInChildren<BaseKey>().ToList();
        }
        
        /*private void Awake()
        {
            keys = GetComponentsInChildren<BaseKey>().ToList();
        }*/

        private void Start()
        {
            keys ??= GetComponentsInChildren<BaseKey>().ToList();
            AssignTextKeyboard();
        }

        #endregion
        
        public void ShowKeyboard(TMP_InputField inputField)
        {
            currentPosition = 0;
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
            if (currentlySelectedInputField.text.Length <= 0)
                return;
            
            var count = Mathf.Abs(currentlySelectedInputField.selectionStringFocusPosition - currentlySelectedInputField.selectionStringAnchorPosition);
            if (count != 0)
            {
                var start = currentlySelectedInputField.selectionStringAnchorPosition < currentlySelectedInputField.selectionStringFocusPosition ? 
                    currentlySelectedInputField.selectionStringAnchorPosition : currentlySelectedInputField.selectionStringFocusPosition;
                
                currentlySelectedInputField.text =
                    currentlySelectedInputField.text.Remove(start, count);
                UpdateCurrentPosition(start);
            }
            else
            {
                if (currentlySelectedInputField.caretPosition == 0)
                {
                    currentlySelectedInputField.text = DeleteCharacter(currentlySelectedInputField.text);
                }
                else
                {
                    var caretPosition = currentlySelectedInputField.caretPosition;
                    currentlySelectedInputField.text =
                        currentlySelectedInputField.text.Remove( caretPosition == 0 ? 0: caretPosition-1, 1);
                    UpdateCurrentPosition(currentPosition-1);
                }
            }
            
            keyboardDisplay.ReplaceDisplayText(currentlySelectedInputField.text);
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

        public void FullResetKeys()
        {
            foreach (var key in keys)
            {
                key.FullResetKey();
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
            
            if (shiftKey)
            {
                ShiftKey();
            }
        }
        
        private void AddString(string newString)
        {
            CheckCaretPosition();
            currentlySelectedInputField.text = currentlySelectedInputField.text.Insert(currentPosition, newString);
            UpdateCurrentPosition(currentPosition+1);
        }

        private void CheckCaretPosition()
        {
            if (currentCaretPosition == currentlySelectedInputField.caretPosition)
                return;

            currentCaretPosition = currentlySelectedInputField.caretPosition;
            UpdateCurrentPosition(currentCaretPosition);
        }

        private void UpdateCurrentPosition(int newPosition)
        {
            if (currentlySelectedInputField.text.Length == 0)
            {
                currentPosition = 0;
                return;
            }
            
            if (newPosition > currentlySelectedInputField.text.Length)
            {
                currentPosition = currentlySelectedInputField.text.Length - 1;
                return;
            }

            currentPosition = newPosition;
        }
        
        private string DeleteCharacter(string currentText)
        {
            if (string.IsNullOrEmpty(currentText))
                return null;
            
            currentText = currentText.Substring(0, currentText.Length - 1);
            currentPosition = currentText.Length;
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
