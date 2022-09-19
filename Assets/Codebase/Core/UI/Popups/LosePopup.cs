using System;
using Codebase.Core.Ads;
using Codebase.Infrastructure.DataStorage;
using Codebase.Infrastructure.Factories;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class LosePopup : Popup
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private GameObject _coinsCounter;

        private IUiFactory _uiFactory;
        private IGameVariables _gameVariables;
        private IAdsModule _adsModule;
        public event Action RetryLevel;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _retryButton.onClick.AddListener(OnRetryButton);

            _uiFactory = AllServices.Container.Single<IUiFactory>();
            _gameVariables = AllServices.Container.Single<IGameVariables>();
            _adsModule = AllServices.Container.Single<IAdsModule>();
            
            //OpenPopup();
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
            _adsModule.ShowInterstitial(RetryCallback, AdPlacements.InterstitialGoRageAfterRestartClick);
        }

        private void RetryCallback(string arg1, string arg2, int arg3)
        {
            BroadcastRetryLevel();
        }

        public void BroadcastRetryLevel()
        {
            RetryLevel?.Invoke();
            //ClosePopup();
        }
    }
}
