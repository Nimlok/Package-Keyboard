using DG.Tweening.Core;
using UnityEngine;

namespace ErrorMessaging
{
    public abstract class BaseInvalidDisplay: MonoBehaviour
    {
        [SerializeField] protected string message;
        [SerializeField] protected Color color;
        [SerializeField] protected ABSAnimationComponent tween;
        
        protected string DefaultMessage;
        protected Color DefaultColor;

        public string SetMessage
        {
            set => message = value;
        }
        
        public abstract void TriggerInvalidMessage();
        public abstract void ResetMessage();
    }
}