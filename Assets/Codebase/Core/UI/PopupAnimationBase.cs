using UnityEngine;

namespace Codebase.Core.UI
{
    public abstract class BasePopupAnimation : MonoBehaviour
    {
        private void Awake()
        {
            OnInitialization();
        }

        protected virtual void OnInitialization()
        {

        }

        public virtual void Play(string animationName)
        {

        }

        public virtual void Play(int animationKey)
        {

        }

        public virtual void SetOpenFlag(bool flag)
        {

        }
    }
}