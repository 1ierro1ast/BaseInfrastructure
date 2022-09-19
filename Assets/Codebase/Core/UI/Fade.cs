using UnityEngine;

namespace Codebase.Core.UI
{
    public class Fade : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Play1 = Animator.StringToHash("Play");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play()
        {
            _animator.SetTrigger(Play1);
        }
    }
}
