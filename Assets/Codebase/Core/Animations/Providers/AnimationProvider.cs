using UnityEngine;

namespace Codebase.Core.Animations.Providers
{
    [RequireComponent(typeof(Animation))]
    public class AnimationProvider : BaseAnimationProvider
    {
        private Animation _animation;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        public override void Play(string name)
        {
            if(_animation == null) _animation = GetComponent<Animation>();
            _animation.Play(name);
        }
    }
}