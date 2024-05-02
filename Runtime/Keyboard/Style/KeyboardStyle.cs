using System;
using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    [Serializable]
    public struct KeyboardImageStyle
    {
        public Sprite sprite;
        public Color color;
    }

    [Serializable]
    public struct KeyboardTextStyle
    {
        public TMP_FontAsset font;
        public Color color;
    }
    
    [Serializable]
    public class KeyboardStyle
    {
        public KeyboardTextStyle keyTextStyle;
        public KeyboardImageStyle keyBackgroundStyle;
        public KeyboardImageStyle keyboardBackgroundStyle;
        public KeyboardImageStyle displayBackgroundStyle;
        public KeyboardTextStyle displayTextStyle;
        public KeyboardTextStyle placeholderTextStyle;
        public KeyboardImageStyle iconStyle;
    }
}

