using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PotatoSDK
{
    public class LionAnalyticsMan : MonoBehaviour, IPotatoInitiatable
    {
        public static int SelectedLevelNumber
        {
            get
            {
                Debug.LogError("Please edit this property to provide the current level number for your game");
                return 9999;
            }

        }
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
