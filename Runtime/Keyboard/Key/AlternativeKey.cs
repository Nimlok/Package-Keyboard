using UnityEngine;

namespace Nimlok.Keyboard.Key
{
    public class AlternativeKey: TextMeshKey
    {
        [Space]
        [SerializeField] private string defaultString;
        [SerializeField] private string alternativeString;

        public override string SetText
        {
            set => defaultString = value;
        }

        protected void Awake()
        {
            keyTextMeshPro.text = defaultString;
        }
        
        public override void ShiftKeyPressed(bool shifted)
        {
            keyTextMeshPro.text = shifted ? alternativeString : defaultString;
        }
        
        public override void ResetKeyToDefault()
        {
            keyTextMeshPro.text = defaultString;
        }
    }
}