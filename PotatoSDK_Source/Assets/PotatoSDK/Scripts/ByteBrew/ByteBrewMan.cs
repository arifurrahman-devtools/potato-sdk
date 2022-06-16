using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if POTATO_BYTEBREW
using ByteBrewSDK;
#endif
namespace PotatoSDK
{
    public class ByteBrewMan : MonoBehaviour, IPotatoInitiatable
    {
        public static ByteBrewMan Instance { get; private set; }
        bool enableModuleLogs = true;
        public string LogColorCode => "2255FF";
        public bool IsReady { get; set; }

        public GameObject prefabReference;

        void IPotatoInitiatable.ForceDisableLogs()
        {
            enableModuleLogs = false;
        }
        void IPotatoInitiatable.InitializeSuperEarly(bool hasConsent, System.Action<IPotatoInitiatable> onModuleReadyToUse)
        {
#if POTATO_BYTEBREW
            Instance = this;
            Instantiate(prefabReference);

            Centralizer.Add_DelayedAct(() => {
                ByteBrew.InitializeByteBrew();
                onModuleReadyToUse?.Invoke(this);
            }, 0.1f);
#else
            onModuleReadyToUse?.Invoke(this);
#endif
        }
    }


}