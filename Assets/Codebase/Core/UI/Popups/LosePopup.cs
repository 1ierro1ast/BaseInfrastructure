using System;
using Codebase.Core.Ads;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class LosePopup : Popup
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private GameObject _coinsCounter;
        
        private IAdsModule _adsModule;
        public event Action RetryLevelEvent;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _retryButton.onClick.AddListener(OnRetryButton);
            _adsModule = AllServices.Container.Single<IAdsModule>();
        }

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _retryButton.interactable = true;
            _coinsCounter.gameObject.SetActive(true);
        }

        private void OnRetryButton()
        {
            _retryButton.interactable = false;
            _coinsCounter.gameObject.SetActive(true);
            _adsModule.ShowInterstitial(RetryCallback, AdPlacements.InterstitialRestartClick);
        }

        private void RetryCallback(string arg1, string arg2, int arg3)
        {
            BroadcastRetryLevel();
        }

        public void BroadcastRetryLevel()
        {
            RetryLevelEvent?.Invoke();
        }
    }
}
