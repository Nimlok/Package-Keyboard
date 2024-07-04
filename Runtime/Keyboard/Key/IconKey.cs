using Keyboard.Style;
using UnityEngine;
using UnityEngine.UI;

namespace Keyboard.Key
{
    public class IconKey : BaseKey
    {
        [SerializeField] private Image icon;
    
        public override void ShiftKeyPressed(bool shifted)
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
