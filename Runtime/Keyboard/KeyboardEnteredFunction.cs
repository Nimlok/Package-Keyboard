using UnityEngine;
using UnityEngine.Events;

namespace UI.Keyboard
{
    public class KeyboardEnteredFunction: MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnterKeyPressed;

        private bool added;
        
        private void OnDisable()
        {
            if (!added)
                return;
            OnScreenKeyboard.onEnterPressed -= () => onEnterKeyPressed?.Invoke();
            added = false;
        }

        public void AddEnterFunction()
        {
            OnScreenKeyboard.onEnterPressed += () => onEnterKeyPressed?.Invoke();
        }
    }
}