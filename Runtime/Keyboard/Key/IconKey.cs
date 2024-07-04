using UI.Keyboard;
using UI.Keyboard.Key;
using UI.Keyboard.Style;
using UnityEngine;
using UnityEngine.UI;

public class IconKey : BaseKey
{
    [SerializeField] private Image icon;
    
    public override void ShiftKeyPressed(bool shifted)
    {
    }

    public override void UpdateStyle(KeyboardStyleObject keyboardStyle)
    {
        if (ignoreStyle)
            return;
        
        UpdateBackgroundImage(keyboardStyle);
        UpdateIconColor(keyboardStyle.iconStyle);
    }
    
    private void UpdateIconColor(KeyboardImageStyle keyboardStyle)
    {
        if (keyboardStyle.color == default)
            return;
        icon.color = keyboardStyle.color;
    }
}
