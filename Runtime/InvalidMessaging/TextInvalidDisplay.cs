using TMPro;
using UnityEngine;

namespace ErrorMessaging
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextInvalidDisplay: BaseInvalidDisplay
    {
        private TextMeshProUGUI invalidTextMesh;

        private void Awake()
        {
            invalidTextMesh = GetComponent<TextMeshProUGUI>();
            DefaultColor = invalidTextMesh.color;
        }

        public override void TriggerInvalidMessage()
        {
            invalidTextMesh.text = message;
            invalidTextMesh.color = color;
        }

        public override void ResetMessage()
        {
            invalidTextMesh.text = string.Empty;
            invalidTextMesh.color = DefaultColor;
        }
    }
}