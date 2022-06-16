using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if POTATO_LION_ANALYTICS
using LionStudios.Suite.Analytics;
#if UNITY_EDITOR
using LionStudios.Suite.Debugging;
#endif
#endif
namespace PotatoSDK
{
    public class LionAnalyticsMan : MonoBehaviour, IPotatoInitiatable
    {

        public string LogColorCode => "FFFF00";

        public bool IsReady { get; set; }

        void IPotatoInitiatable.InitializeSuperEarly(bool hasConsent, Action<IPotatoInitiatable> onModuleReadyToUse)
        {
            IsReady = true;
            onModuleReadyToUse?.Invoke(this);
        }



        void IPotatoInitiatable.ForceDisableLogs()
        {
        }
    }

}
