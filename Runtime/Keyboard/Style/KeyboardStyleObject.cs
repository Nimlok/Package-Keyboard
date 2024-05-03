
using System;
using UnityEngine;

namespace UI.Keyboard.Style
{
    [CreateAssetMenu(menuName = "KeyboardStyle")]
    public class KeyboardStyleObject : ScriptableObject
    {
        public KeyboardTextStyle keyTextStyle;
        public KeyboardImageStyle keyBackgroundStyle;
        public KeyboardImageStyle keyboardBackgroundStyle;
        public KeyboardImageStyle displayBackgroundStyle;
        public KeyboardTextStyle displayTextStyle;
        public KeyboardTextStyle placeholderTextStyle;
        public KeyboardImageStyle iconStyle;

        public Action OnObjectChange;

        private void OnDisable()
        {
            OnObjectChange = null;
        }

        private void OnValidate()
        {
            OnObjectChange?.Invoke();
            Debug.Log($"keyboard Style object updated");
        }
    }
}
