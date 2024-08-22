using System;
using TMPro;
using UnityEngine;

namespace Nimlok.Keyboard.Style
{
    [Serializable]
    public class IconKeyStyle
    {
        public Color color;
    }

    [Serializable]
    public class BackgroundStyle
    {
        public Sprite imageOff;
        public Sprite imageOn;
        public Color color;
    }

    [Serializable]
    public class TextKeyStyle
    {
        public TMP_FontAsset font;
        public Color color;
    }
    
    [Serializable]
    public class KeyboardStyle
    {
        public TextKeyStyle keyTextKeyStyle;
        public IconKeyStyle keyBackgroundStyle;
        public BackgroundStyle backgroundStyle;
        public IconKeyStyle displayBackgroundStyle;
        public TextKeyStyle displayTextKeyStyle;
        public TextKeyStyle placeholderTextKeyStyle;
        public IconKeyStyle iconStyle;
    }
}

