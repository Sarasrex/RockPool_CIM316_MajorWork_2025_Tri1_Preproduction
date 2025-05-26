using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCutsceneCheck : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;

    public void StartGame()
    {
        Debug.Log(" Loading CutSceneLayer from main menu.");
        sceneSwitcher.LoadSceneByName("CutSceneLayer");
    }
}
