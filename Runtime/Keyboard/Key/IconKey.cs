using Nimlok.Keyboard.Style;
using UnityEngine;
using UnityEngine.UI;

namespace Nimlok.Keyboard.Key
{
    public class IconKey : BaseKey
    {
        [SerializeField] private Image icon;

        public override void SetText(string text)
        {
            
        }

        public override void SeTextDefault(string text)
        {
            
        }

        public override void ShiftKeyPressed(bool shifted)
        {
        }

        public override void ResetKeyToDefault()
        {
        }

        public override void FullResetKey()
        {
            
        }

        public override void UpdateStyle(KeyboardStyleObject keyboardStyle)
        {
            UpdateBackgroundImage(keyboardStyle.keyBackgroundStyle);
            UpdateIconColor(keyboardStyle.iconStyle);
        }
    
        private void UpdateIconColor(IconKeyStyle style)
        {
            if (style.color == default)
                return;
            icon.color = style.color;
        }
    }
}
