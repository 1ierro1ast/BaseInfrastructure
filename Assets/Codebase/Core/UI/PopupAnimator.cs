using System;
using Codebase.Core.Animations;
using UnityEngine;

namespace Codebase.Core.UI
{
    public class PopupAnimator : MonoBehaviour
    {
        [SerializeField] private string _openAnimationName = "Open";
        [SerializeField] private string _closeAnimationName = "Close";

        private BaseAnimationProvider _animationProvider;

        private void Awake()
        {
            _animationProvider = GetComponent<BaseAnimationProvider>();
            if (_animationProvider == null)
            {
                throw new Exception($"AnimationProvider not exists on {gameObject.name}");
            }
        }

        public void SetOpenFlag(bool flag)
        {
            if (_animationProvider == null) _animationProvider = GetComponent<BaseAnimationProvider>();

            if (flag)
                _animationProvider.Play(_openAnimationName);
            else
                _animationProvider.Play(_closeAnimationName);
        }
    }
}