
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PotatoSDK
{
    [CustomEditor(typeof(LionAnalyticsMan))]
    public class LionAnalyticsMan_Editor : SymbolControlledModuleEditor
    {

        public override string OnCoreModuleGUI()
        {
#if POTATO_GAME_ANALYTICS
            base.OnCoreModuleGUI();
#endif
            return "POTATO_LION_ANALYTICS";
        }
    }
}