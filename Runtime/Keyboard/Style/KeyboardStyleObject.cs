using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nimlok.Keyboard.Style
{
    [CreateAssetMenu(menuName = "OnScreenKeyboard/KeyboardStyle")]
    public class KeyboardStyleObject : ScriptableObject
    {
        [FoldoutGroup("Key")]
        [TitleGroup("Key/Text"), HideLabel]public TextKeyStyle keyTextKeyStyle;
        [TitleGroup("Key/Icon"), HideLabel]public IconKeyStyle iconStyle;
        [TitleGroup("Key/Background"), HideLabel]public BackgroundStyle keyBackgroundStyle;
        
        [FoldoutGroup("Input Text Display")]
        [BoxGroup("Input Text Display/Text"), HideLabel]public TextKeyStyle displayTextKeyStyle;
        [BoxGroup("Input Text Display/Background"), HideLabel]public IconKeyStyle displayBackgroundStyle;
        
        [FoldoutGroup("Input Name Display")]
        [BoxGroup("Input Name Display/Text"), HideLabel]public TextKeyStyle placeholderTextKeyStyle;
        [BoxGroup("Input Name Display/Background"), HideLabel]public IconKeyStyle placeHolderBackgroundStyle;
        
        [TitleGroup("Keyboard Background"), HideLabel] public IconKeyStyle backgroundStyle;

        public Action OnObjectChange;

        private void OnDisable()
        {
            OnObjectChange = null;
        }

        private void OnValidate()
        {
            OnObjectChange?.Invoke();
        }
    }
}
