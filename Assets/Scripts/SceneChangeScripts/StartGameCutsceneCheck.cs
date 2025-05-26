using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCutsceneCheck : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;

    public void StartGame()
    {
        if (!CutsceneTracker.HasSeen("Intro"))
        {
            CutsceneRouter.cutsceneID = "Intro";
            CutsceneTracker.MarkSeen("Intro");
            sceneSwitcher.LoadSceneByName("CutSceneLayer");
        }
        else
        {
            sceneSwitcher.LoadSceneByName("Foyer");
        }
    }
}
