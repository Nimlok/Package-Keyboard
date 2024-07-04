using UnityEngine;

namespace Keyboard.Key
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

        protected override void Awake()
        {
            keyTextMeshPro.text = defaultString;
            base.Awake();
        }

        public override void ShiftKeyPressed(bool shifted)
        {
            keyTextMeshPro.text = shifted ? alternativeString : defaultString;
        }
    }
}