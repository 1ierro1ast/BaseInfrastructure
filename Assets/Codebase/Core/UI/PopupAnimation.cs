using System;
using UnityEngine;

namespace Codebase.Core.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Animation))]
    public class PopupAnimation : BasePopupAnimation
    {
        [SerializeField] AnimationClip _openAnimation;
        [SerializeField] AnimationClip _closeAnimation;
        [SerializeField] private Animation _animation;

        private void OnValidate()
        {
            if (_animation == null)
                _animation = GetComponent<Animation>();

            if (_animation.GetClipCount() > 0) return;

            _animation.AddClip(_openAnimation, _openAnimation.name);
            _animation.AddClip(_closeAnimation, _closeAnimation.name);
        }

        protected override void OnInitialization()
        {
        }

        public override void SetOpenFlag(bool flag)
        {
            if (flag)
                Play(_openAnimation.name);
            else
                Play(_closeAnimation.name);
        }

        public override void Play(string animationName)
        {
            _animation.Play(animationName);
        }
    }
}