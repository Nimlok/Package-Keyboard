using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core;
using Keyboard.Key;
using TMPro;
using UI.Keyboard.Key;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Keyboard
{
    [ExecuteInEditMode]
    public class OnScreenKeyboard : MonoBehaviour
    {
        [SerializeField] private KeyboardDisplay keyboardDisplay;
        [SerializeField] private ABSAnimationComponent showTween;
        
        private bool shiftKey;
        private bool onShow;
        private EventSystem eventSystem;
        private TMP_InputField currentlySelectedInputField;
        private List<BaseKey> keys = new List<BaseKey>();
        private TMP_InputField.ContentType contentType;

        public List<BaseKey> GetKeys => keys;
        
        public static Action<TextMeshKey> onKeyPressed;
        public static Action onEnterPressed;

        #region Unity Functions
        private void OnValidate()
        {
            keys ??= GetComponentsInChildren<BaseKey>().ToList();
        }

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
            keys = GetComponentsInChildren<BaseKey>().ToList();
            eventSystem = EventSystem.current;
            SetAllInputFields();
        }
        #endregion
        
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

        public void GoKey()
        {
            onEnterPressed?.Invoke();
        }

        public void BackspaceKey()
        {
            var newDeletedCharacter = DeleteCharacter(currentlySelectedInputField.text);
            keyboardDisplay.ReplaceDisplayText(newDeletedCharacter);
            currentlySelectedInputField.text = newDeletedCharacter;
        }
        
        public void ClearKeyboard()
        {
            keyboardDisplay.ClearText();
        }

        public BaseKey FindKey(string key)
        {
            return keys.Find(x => string.CompareOrdinal(x.GetText, key) == 0);
        }
        
        public void MoveNavigation()
        {
            if (currentlySelectedInputField == null)
                return;

            var nextObject = currentlySelectedInputField.FindSelectableOnDown();
            var inputfield = nextObject.GetComponent<TMP_InputField>();
            if (nextObject == null || inputfield == null)
            {
                HideKeyboard();
                return;
            }
            
            nextObject.Select();
        }
        
        private void ShowKeyboard()
        {
            AssignCurrentlySelectedInput();
            PlayKeyboardTransition();
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
        
        private void SelectedInput(TMP_InputField inputField)
        {
            if (inputField == null)
            {
                Debug.LogError($"{name} Missing Inputfield");
                return;
            }
            
            currentlySelectedInputField = inputField;
            contentType = currentlySelectedInputField.contentType;
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

        private void SetAllInputFields()
        {
            var inputfields = FindObjectsOfType<TMP_InputField>(true);
            foreach (var inputfield in inputfields)
            {
                inputfield.onSelect.AddListener((c) => ShowKeyboard());
            }
        }
        
        private void AssignCurrentlySelectedInput()
        {
            if (eventSystem == null || eventSystem.currentSelectedGameObject == null)
                return;
            
            var inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
            if (inputField == null)
                return;

            SelectedInput(inputField);
        }
    }
}
