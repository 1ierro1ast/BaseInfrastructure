using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Core.Ads
{
    public interface IAdsModule : IService, INetworkChecker
    {
        public bool AdsIsEnable { get; }
        public bool RewardedAdsIsReady { get; }
        public bool InterstitialAdsIsReady { get; }
        
        public void Initialize();
        public void LoadBanner();
        public void ShowReward(Action<string, string, int> callback, string adPlacement, int amount);
        public void ShowInterstitial(Action<string, string, int> callback, string adPlacement);
    }
}