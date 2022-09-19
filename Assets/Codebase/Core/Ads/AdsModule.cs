using System;
using UnityEngine;

namespace Codebase.Core.Ads
{
    public class AdsModule : IAdsModule
    {
        public bool AdsIsEnable { get; private set; }
        public bool RewardedAdsIsReady => true;//FunGamesMax.IsRewardedAdReady();

        public bool InterstitialAdsIsReady => true;//FunGamesMax.IsInterstitialReady;
        public bool NetworkIsEnable => Application.internetReachability == NetworkReachability.NotReachable;


        public AdsModule()
        {
            Initialize();
        }

        public void Initialize()
        {
            AdsIsEnable = true;
            LoadBanner();
        }

        public void LoadBanner()
        {
            if (AdsIsEnable)
                Debug.Log("Show banner");//FunGamesMax.ShowBannerAd();
            else
                Debug.Log("Hide banner");//FunGamesMax.HideBannerAd();
        }

        public void ShowReward(Action<string, string, int> callback, string adPlacement, int amount)
        {
            if (AdsIsEnable && RewardedAdsIsReady)
            {
                Debug.Log("Show Reward");
                callback("success", "", amount);
                //FunGamesMax.ShowRewarded(callback, adPlacement, adPlacement, amount);
            }
            else
            {
                callback("success", "", amount);
            }
        }

        public void ShowInterstitial(Action<string, string, int> callback, string adPlacement)
        {
            if (AdsIsEnable && InterstitialAdsIsReady)
            {
                Debug.Log("Show Interstitial");
                callback("success", "", 0);
                //FunGamesMax.ShowInterstitial(callback, adPlacement, adPlacement);
            }
            else
            {
                callback("success", "", 0);
            }
        }
        
    }
}