using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Lightweight static courier for cutscene routing data between the origin scene and CutSceneLayer.
/// I set these right before I load the CutSceneLayer.
public static class CutsceneRequest
{
    public static string CutsceneID = "Intro";     // Which cutscene to play
    public static string ReturnSceneName = "";     // Where to go after it completes
    public static bool   ForcePlay = false;        // If true, ignore PlayerPrefs skip

    // which hermit to unlock after the cutscene returns
    public static string NextUnlockHermit = null;


    public static void Set(string id, string returnScene, bool forcePlay = false)
    {
        CutsceneID = id;
        ReturnSceneName = returnScene;
        ForcePlay = forcePlay;
        NextUnlockHermit = null; // clear by default

#if UNITY_EDITOR
        Debug.Log($"[CutsceneRequest] Set -> id={id}, return={returnScene}, force={forcePlay}");
#endif
    }
}
