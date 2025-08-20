using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Main Menu "Start" button hook.
/// Now supports clearing all PlayerPrefs and resetting in-memory progress
/// so every new game starts fresh (great for exhibition builds).
public class StartGameCutsceneCheck : MonoBehaviour
{
    [Header("What to play first")]
    public string introCutsceneId = "Intro";   // must exist in CutsceneRouter
    public string returnSceneName = "Foyer";   // where I want to land afterwards
    public bool forcePlayIntro = false;        // set true to ignore PlayerPrefs while testing

    [Header("Cutscene Scene")]
    public string cutsceneLayerScene = "CutSceneLayer";

    [Header("Exhibition mode")]
    [Tooltip("If true, clears ALL PlayerPrefs (cutscene flags, options, etc.) and resets GameProgress on Start.")]
    public bool deleteAllPrefsOnStart = true;

    // Hook this to the Main Menu Start button
    public void StartGame()
    {
        if (deleteAllPrefsOnStart)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            if (GameProgress.Instance != null)
                GameProgress.Instance.ResetProgress();
        }

        CutsceneRequest.Set(introCutsceneId, returnSceneName, forcePlayIntro);
        SceneSwitcher.Load(cutsceneLayerScene);
    }
}
