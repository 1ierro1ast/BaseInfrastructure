using UnityEngine;

namespace Codebase.Core.UI.Counters
{
    public class LoadingProgressHandler : MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        private void OnEnable()
        {
            _progressBar.SetProgress(0);
        }
    }
}
