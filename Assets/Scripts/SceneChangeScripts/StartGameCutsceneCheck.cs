using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// Main Menu "Start" button hook.
/// I request the Intro cutscene and jump to CutSceneLayer. Router handles the rest.
public class StartGameCutsceneCheck : MonoBehaviour
{
    [Header("What to play first")]
    public string introCutsceneId = "Intro";   // must exist in CutsceneRouter
    public string returnSceneName = "Foyer";   // where I want to land afterwards
    public bool forcePlayIntro = false;        // set true to ignore PlayerPrefs while testing

    [Header("Cutscene Scene")]
    public string cutsceneLayerScene = "CutSceneLayer";

    // Hook this to the Main Menu Start button
    public void StartGame()
    {
        CutsceneRequest.Set(introCutsceneId, returnSceneName, forcePlayIntro);
        SceneSwitcher.Load(cutsceneLayerScene);
    }
}

