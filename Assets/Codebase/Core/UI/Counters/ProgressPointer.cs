using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Counters
{
    public class ProgressPointer : MonoBehaviour
    {
        [SerializeField] private Vector2 imageSize;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;

        private void Update()
        {
            rectTransform.anchoredPosition = new Vector2(
                GetValueByPercent(image.fillAmount, imageSize.x), 0);
        }

        private float GetValueByPercent(float percent, float fullValue)
        {
            return fullValue * percent;
        }
    }
}