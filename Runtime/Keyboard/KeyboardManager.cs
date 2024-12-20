using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nimlok.Keyboard
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
            #if !UNITY_EDITOR && UNITY_ANDROID || UNITY_IOS
            return;
            #endif
            
            eventSystem = EventSystem.current;
            SetAllInputFields();
        }

        public void HideKeyboard()
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
            var inputFields = FindObjectsByType<TMP_InputField>(FindObjectsSortMode.None);
            foreach (var inputField in inputFields)
            {
                inputField.onSelect.AddListener((c) => ShowKeyboard());
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