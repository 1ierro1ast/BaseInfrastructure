using System;
using Codebase.Core.Ads;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class WinPopup : Popup
    {
        [SerializeField] private Button _claimCoinsNoMultiplier;
        [SerializeField] private GameObject _coinsCounter;
        
        private IAdsModule _adsModule;
        public event Action NextLevel;
        public event Action ButtonClicked;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _claimCoinsNoMultiplier.onClick.AddListener(OnClaimCoinsNoMultiplier);
            _adsModule = AllServices.Container.Single<IAdsModule>();
        }

        private void OpenPopupCallback(string arg1, string arg2, int arg3)
        {
            Debug.Log("Interstitial is done");
        }

        private void OnEnable()
        {
            ValidateAds();
        }

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _coinsCounter.gameObject.SetActive(true);
            _claimCoinsNoMultiplier.interactable = true;
            _adsModule.ShowInterstitial(OpenPopupCallback, AdPlacements.InterstitialNextLevel);
        }

        private void ValidateAds()
        {
            if (!_adsModule.RewardedAdsIsReady)
            {
                //_claimMultipliedCoinsWithAds.interactable = false;
            }
        }

        private void OnClaimCoinsNoMultiplier()
        {
            _claimCoinsNoMultiplier.interactable = false;
            _coinsCounter.gameObject.SetActive(false);
            ButtonClicked?.Invoke();
            //_coinsFxFactory.CreateCoinsFromCanvas(_claimCoinsNoMultiplier.transform.position, 30, 0.05f);
            Invoke(nameof(TryGoToNextLevel), 2f);
        }

        private void TryGoToNextLevel()
        {
            BroadcastNextLevel();
        }

        public void BroadcastNextLevel()
        {
            NextLevel?.Invoke();
        }
    }
}