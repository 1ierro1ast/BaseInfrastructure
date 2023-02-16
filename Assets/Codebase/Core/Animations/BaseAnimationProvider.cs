using UnityEngine;

namespace Codebase.Core.Animations
{
    public abstract class BaseAnimationProvider : MonoBehaviour
    {
        public abstract void Play(string name);
    }
}