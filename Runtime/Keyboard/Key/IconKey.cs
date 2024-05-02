using UI.Keyboard;
using UI.Keyboard.Key;
using UnityEngine;
using UnityEngine.UI;

public class IconKey : BaseKey
{
    [SerializeField] private Image icon;
    
    //TODO: DS 2/05/24 either remove abstract from base class or find uses
    public override void KeyPressed()
    {
    }

    public override void ShiftKeyPressed(bool shifted)
    {
    }

    public override void UpdateStyle(KeyboardStyle keyboardStyle)
    {
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
