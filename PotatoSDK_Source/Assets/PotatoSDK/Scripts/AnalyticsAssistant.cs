using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PotatoSDK;
using System;

#if POTATO_GAME_ANALYTICS
using GameAnalyticsSDK;
#endif

#if POTATO_LION_ANALYTICS
using LionStudios.Suite.Analytics;
#if UNITY_EDITOR
using LionStudios.Suite.Debugging;
#endif
#endif


#if POTATO_BYTEBREW
using ByteBrewSDK;
#endif
public partial class AnalyticsAssistant : MonoBehaviour , IPotatoInitiatable
{
    public static AnalyticsAssistant Instance { get; private set; }
    public string LogColorCode => "FF00FF";

    public bool IsReady { get; set; }

    bool logDisabled = false;
    void Log(string str)
    {
        if (!logDisabled) str.Log(LogColorCode);
    }
    void IPotatoInitiatable.ForceDisableLogs()
    {
        logDisabled = true;
    }
    void IPotatoInitiatable.InitializeSuperEarly(bool hasConsent, Action<IPotatoInitiatable> onModuleReadyToUse)
    {
        IsReady = true;
        onModuleReadyToUse?.Invoke(this);

        Log("Analytics Assistant Initialized1");
        Potato.ExecuteWhenPotatoReady(() => {
            Log("Analytics Assistant Initialized2");
#if POTATO_LION_ANALYTICS
#if UNITY_EDITOR
            LionDebugger.Hide();
#endif


            LionAnalytics.GameStart();

#endif
            Instance = this;
            Log("Analytics Assistant Initialized");
        });
    }

#if POTATO_GAME_ANALYTICS || POTATO_LION_ANALYTICS || POTATO_BYTEBREW
    [Header("Enable Setting for predefined Logs")]
#if POTATO_GAME_ANALYTICS
    public bool gameAnalytics_logsEnabled = true;
#endif
#if POTATO_LION_ANALYTICS
  public bool lionAnalytics_logsEnabled = true;
#endif

#if POTATO_BYTEBREW
    public bool byteBrew_logsEnabled = true;
#endif
#endif


    public void LevelStarted()
    {
        Log($"started {SelectedLevelNumber}");
#if POTATO_BYTEBREW
        if (byteBrew_logsEnabled) ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Started, "level", SelectedLevelNumber.ToString());
#endif
#if POTATO_LION_ANALYTICS
        if (lionAnalytics_logsEnabled) LionAnalytics.LevelStart(SelectedLevelNumber, 1);
#endif
#if POTATO_GAME_ANALYTICS
        if (gameAnalytics_logsEnabled) GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, SelectedLevelNumber.ToString());
#endif
    }
    public void LevelCompleted()
    {
        Log($"completed {SelectedLevelNumber}");
#if POTATO_BYTEBREW
        if (byteBrew_logsEnabled) ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Completed, "level", SelectedLevelNumber.ToString());
#endif
#if POTATO_LION_ANALYTICS
        if (lionAnalytics_logsEnabled) LionAnalytics.LevelComplete(SelectedLevelNumber, 1);
#endif
#if POTATO_GAME_ANALYTICS
        if (gameAnalytics_logsEnabled) GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, SelectedLevelNumber.ToString());
#endif
    }
    public void LevelFailed()
    {
        Log($"failed {SelectedLevelNumber}");
#if POTATO_BYTEBREW
        if (byteBrew_logsEnabled) ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Failed, "level", SelectedLevelNumber.ToString());
#endif
#if POTATO_LION_ANALYTICS
        if (lionAnalytics_logsEnabled) LionAnalytics.LevelFail(SelectedLevelNumber, 1);
#endif
#if POTATO_GAME_ANALYTICS
        if (gameAnalytics_logsEnabled) GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, SelectedLevelNumber.ToString());
#endif
    }
    public void LevelRestarted()
    {
        Log($"restarted {SelectedLevelNumber}");
#if POTATO_BYTEBREW
        if (byteBrew_logsEnabled) ByteBrew.NewCustomEvent( "Restart",$"Level_{SelectedLevelNumber}");
#endif
#if POTATO_LION_ANALYTICS
        if (lionAnalytics_logsEnabled) LionAnalytics.LevelRestart(SelectedLevelNumber, 1);
#endif
#if POTATO_GAME_ANALYTICS
        if (gameAnalytics_logsEnabled) GameAnalytics.NewDesignEvent($"Restart:Level_{SelectedLevelNumber}");
#endif
    }

    


    

}



