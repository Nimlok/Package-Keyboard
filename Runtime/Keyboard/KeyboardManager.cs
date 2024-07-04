using TMPro;
using UI.Keyboard;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Keyboard
{
    public class KeyboardManager : MonoBehaviour
    {
        [SerializeField] private OnScreenKeyboard fullKeyboard;
        [SerializeField] private OnScreenKeyboard numpad;
    
        private EventSystem eventSystem;
        private TMP_InputField currentlySelectedInputField;
        private OnScreenKeyboard currentKeyboard;
    
        private void Awake()
        {
            eventSystem = EventSystem.current;
            SetAllInputFields();
        }

        private void HideKeyboard()
        {
            if (currentKeyboard == null)
                return;
        
            currentKeyboard.HideKeyboard();
            currentKeyboard = null;
        }
    
        private void ShowKeyboard()
        {
            AssignCurrentlySelectedInput();
            if (currentlySelectedInputField.contentType == TMP_InputField.ContentType.Alphanumeric)
            {
                if (currentKeyboard != null && currentKeyboard != numpad)
                {
                    currentKeyboard.HideKeyboard();
                }

                currentKeyboard = numpad;
                numpad.ShowKeyboard(currentlySelectedInputField);
            }
            else
            {
                if (currentKeyboard != null && currentKeyboard != fullKeyboard)
                {
                    currentKeyboard.HideKeyboard();
                }

                currentKeyboard = fullKeyboard;
                fullKeyboard.ShowKeyboard(currentlySelectedInputField);
            }
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
            {
                HideKeyboard();
                return;
            }
            

            SelectedInput(inputField);
        }
    
        private void SelectedInput(TMP_InputField inputField)
        {
            if (inputField == null)
            {
                Debug.LogError($"{name} Missing Inputfield");
                return;
            }
            
            currentlySelectedInputField = inputField;
        }
    }
}