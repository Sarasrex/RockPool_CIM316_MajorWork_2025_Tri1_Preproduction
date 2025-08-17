using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Use this ONLY for cutscenes inside CutSceneLayer.
/// I advance through ordered stills; on the last one I notify the router.
public class PanelController_Cutscene : MonoBehaviour
{
    [Tooltip("Order matters: element 0 shows first.")]
    public GameObject[] panels;

    private int currentIndex = -1;

    private void Start()
    {
        if (panels == null || panels.Length == 0)
        {
            Debug.LogWarning("[PanelController_Cutscene] No panels assigned.");
            return;
        }
        Show(0);
    }

    public void Next()
    {
        if (panels == null || panels.Length == 0) return;

        int next = currentIndex + 1;
        if (next < panels.Length) Show(next);
        else Finish();
    }

    public void Prev()
    {
        if (panels == null || panels.Length == 0) return;

        int prev = currentIndex - 1;
        if (prev >= 0) Show(prev);
    }

    private void Show(int index)
    {
        for (int i = 0; i < panels.Length; i++)
            if (panels[i] != null) panels[i].SetActive(i == index);

        currentIndex = index;
    }

    private void Finish()
    {
        if (CutsceneRouter.Instance != null)
            CutsceneRouter.Instance.CompleteCutscene();
        else
            Debug.LogWarning("[PanelController_Cutscene] No CutsceneRouter found.");
    }
}
