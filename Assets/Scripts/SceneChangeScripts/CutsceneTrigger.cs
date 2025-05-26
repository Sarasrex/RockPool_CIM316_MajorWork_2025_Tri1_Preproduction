using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutsceneTrigger : MonoBehaviour
{
    [Tooltip("This should match the name of the Cutscene GameObject inside CutSceneLayer (e.g., Cutscene_Priscilla)")]
    public string cutsceneID = "Priscilla";

    // Call this method via Button OnClick() to route to the correct cutscene
    public void SetCutsceneID()
    {
        CutsceneRouter.cutsceneID = cutsceneID;
        Debug.Log("Cutscene ID set to: " + cutsceneID);
    }
}

