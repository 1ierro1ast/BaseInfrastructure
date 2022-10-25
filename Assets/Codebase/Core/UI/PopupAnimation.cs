using System.Runtime.CompilerServices;
using UnityEngine;

namespace Codebase.Core.UI
{
    [RequireComponent(typeof(Animation))]
    public class PopupAnimation : BasePopupAnimation
    {
        private Animation _animation;

        protected override void OnInitialization()
        {
            _animation = GetComponent<Animation>();
        }
    }
}