using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Counters
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fillBar;

        public void SetProgress(float amount)
        {
            _fillBar.fillAmount = amount;
        }

        public void SetProgress(float current, float full)
        {
            _fillBar.fillAmount = current / full;
        }

        public void SetProgress(int current, int full)
        {
            _fillBar.fillAmount = current / (float)full;
        }
    }
}
