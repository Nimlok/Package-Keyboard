using System;
using TMPro;
using UnityEngine;

namespace UI.Keyboard
{
    [Serializable]
    public struct KeyboardImageStyle
    {
        public Color color;
        public Sprite sprite;
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
        public KeyboardImageStyle keyImageStyle;
        public KeyboardImageStyle keyboardImageStyle;
        public KeyboardImageStyle displayImageStyle;
        public KeyboardTextStyle displayTextStyle;
        public KeyboardTextStyle placeholderTextStyle;
    }
}

