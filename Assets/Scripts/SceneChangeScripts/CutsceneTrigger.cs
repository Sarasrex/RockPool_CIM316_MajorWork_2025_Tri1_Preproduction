using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Attach to any button in a gameplay/menu scene to request a cutscene and jump into CutSceneLayer.
/// I don’t load the return scene here; that’s handled after the cutscene completes.
public class CutsceneTrigger : MonoBehaviour
{
    [Header("Routing")]
    [Tooltip("Router key/ID (Intro, CaptainClaw, Pearl, Finn, Hans, Priscilla, Final)")]
    public string cutsceneID = "Intro";

    [Tooltip("The scene I want to return to after the cutscene finishes")]
    public string returnSceneName = "Foyer";

    [Tooltip("If true, I always play even if already seen")]
    public bool forcePlay = false;

    [Header("Cutscene Scene")]
    [Tooltip("The dedicated scene that contains CutsceneRouter and the cutscene roots")]
    public string cutsceneLayerScene = "CutSceneLayer";

    // Hook this to the UI Button OnClick()
    public void PlayCutscene()
    {
        if (string.IsNullOrEmpty(cutsceneLayerScene))
        {
            Debug.LogError("[CutsceneTrigger] Cutscene layer scene name is not set.");
            return;
        }

        CutsceneRequest.Set(cutsceneID, returnSceneName, forcePlay);
        SceneSwitcher.Load(cutsceneLayerScene);
    }
}
