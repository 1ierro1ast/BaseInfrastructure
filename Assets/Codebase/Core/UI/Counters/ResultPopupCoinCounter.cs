using System.Collections;
using TMPro;
using UnityEngine;

namespace Codebase.Core.UI.Counters
{
    public class ResultPopupCoinCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _counterDuration;
        

        private void OnEnable()
        {
            
        }

        private IEnumerator CountCoins(int finalAmount, int startAmount = 0)
        {
            int currentAmount = startAmount;
            float delay = _counterDuration / finalAmount;
            while (currentAmount < finalAmount)
            {
                currentAmount++;
                _text.text = currentAmount + "";
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator CountCoinsCoroutine(int finalAmount, int startAmount = 0, float duration = 1)
        {
            int currentAmount = startAmount;

            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                currentAmount = Mathf.CeilToInt(Mathf.Lerp(
                    (float) currentAmount,
                    (float) finalAmount,
                    time / duration));
                _text.text = currentAmount + "";

                yield return null;
            }

            _text.text = currentAmount + "";
        }
    }
}