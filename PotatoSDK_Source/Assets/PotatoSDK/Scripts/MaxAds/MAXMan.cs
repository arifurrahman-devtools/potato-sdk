using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoSDK
{
    public class MAXMan : MonoBehaviour, IPotatoInitiatable
    {
        public static MAXMan Instance { get; set; }

        public LogLevel ActiveLogLevel;
        public bool showMaxMediationDebugger;
        public bool enableTestUI = true;
        public bool enableBannerSafeZone;
        [Space]
        public float skipTimeBetweenInterstitials = 15;
        public float skipTimeAfterRV = 15;
        [Space]
        public IDKeeper interstitialUnitID;
        public IDKeeper rewardedAdUnitID;
        public IDKeeper bannerAdUnitID;
        [Space]
        [Space]
        public GameObject adsHelperCanvasPrefab;
        MAXAdsHelperCanvasController helperCanvas;

        public string LogColorCode => "9a8ffd";
        public bool IsReady { get; set; }
        void IPotatoInitiatable.ForceDisableLogs()
        {
            ActiveLogLevel = LogLevel.None;
        }

#if !POTATO_MAX
        void IPotatoInitiatable.InitializeSuperEarly(bool hasConsent, System.Action<IPotatoInitiatable> onModuleReadyToUse)
        {

            onModuleReadyToUse?.Invoke(this);
        }
#else

        void IPotatoInitiatable.InitializeSuperEarly(bool hasConsent, System.Action<IPotatoInitiatable> onModuleReadyToUse)
        {
            SkipInterstitialUntil = 0;

            if ((int)ActiveLogLevel >= (int)LogLevel.Important) "Max init".Log(LogColorCode);
            if (interstitialUnitID.isValid) interstitial = new MaxInterstitial(interstitialUnitID.id, OnInterstitialComplete);
            if (rewardedAdUnitID.isValid) rewarded = new MaxRewarded(rewardedAdUnitID.id,OnRVComplete);
            if (bannerAdUnitID.isValid) banner = new MaxBanner(bannerAdUnitID.id, Color.white);
            if (showMaxMediationDebugger) Centralizer.Add_DelayedAct(MaxSdk.ShowMediationDebugger, 5);


            helperCanvas = Instantiate(adsHelperCanvasPrefab).GetComponent<MAXAdsHelperCanvasController>();
            helperCanvas.transform.SetParent(this.transform);
            helperCanvas.testCanvas.SetActive(enableTestUI);
            helperCanvas.bannerSafeZone.SetActive(enableBannerSafeZone);

            if (helperCanvas)
            {
                helperCanvas.showBanner.onClick.AddListener(ShowBanner);
                helperCanvas.hideBanner.onClick.AddListener(HideBanner);
                helperCanvas.showInterStitial.onClick.AddListener(ShowInterstitial);
                helperCanvas.showRV.onClick.AddListener(TestRV);
            }
            Instance = this;
            onModuleReadyToUse?.Invoke(this);
            IsReady = true;
        }

        public static MaxInterstitial interstitial;
        public static MaxRewarded rewarded;
        public static MaxBanner banner;

        #region interstitial skipping       

        public static float SkipInterstitialUntil { get; private set; }
        public static void Set_MinimumInterstitialGap(float value)
        {
            Instance.skipTimeBetweenInterstitials = value;
        }
        public static void Set_InterstitialSkipTimeAfterRV(float value)
        {
            Instance.skipTimeAfterRV = value;
        }
        private void InterstitialSkipTime_SuggestNewLimit(float newTimeLimit)
        {
            if (newTimeLimit > SkipInterstitialUntil)
            {
                SkipInterstitialUntil = newTimeLimit;
            }
        }
        private void OnInterstitialComplete()
        {
            InterstitialSkipTime_SuggestNewLimit(Time.realtimeSinceStartup + skipTimeBetweenInterstitials);
        }
        private void OnRVComplete()
        {
            InterstitialSkipTime_SuggestNewLimit(Time.realtimeSinceStartup + skipTimeAfterRV);
        }
        #endregion

        private void TestRV()
        {
            ShowRV(RVPlacement.test,(bool success) =>
            {
                if((int)ActiveLogLevel >= (int)LogLevel.Important) string.Format("RV completed with success result: {0}",success).Log(LogColorCode);
            });
        }
        public static void ShowInterstitial()
        {
            if (interstitial!=null)
            {
                interstitial.ShowAd();
            }
            else Debug.LogError("interstitial AD not setup!");
        }
        public static void ShowRV(RVPlacement placement, Action<bool> onComplete)
        {
            if (rewarded != null)
            {
                rewarded.ShowAd(placement.ToString(), onComplete);
            }
            else
            {
                Debug.LogError("rewarded AD not setup!");
                onComplete?.Invoke(false);
            }
        }
        public static void ShowBanner()
        {
            if (banner != null)
            {
                banner.SetActive(true);
            }
            else Debug.LogError("banner AD not setup!");
        }
    
        public static void HideBanner()
        {
            if (banner != null)
            {
                banner.SetActive(false);
            }
            else Debug.LogError("banner AD not setup!");
        }


#endif
    }

    public enum RVPlacement
    {
        test =-1,
        bonus_level=1,
        coin_multiplier=2,
        energy_refill=3,
        room_customization=10,
        skin_customization=11,
    }
}
