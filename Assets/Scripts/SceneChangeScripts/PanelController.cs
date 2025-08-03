using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// This script is for the cutscenes and next panel buttons

public class PanelController : MonoBehaviour
{
    public GameObject[] panels;
    public string sceneToLoadWhenDone = ""; // Leave empty for default unload
    private int currentIndex = 0;

    void Start()
    {
        ShowPanel(0);
    }

    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }

        currentIndex = index;
    }

    public void NextPanel()
    {
        int next = currentIndex + 1;

        if (next < panels.Length)
        {
            ShowPanel(next);
        }
        else
        {
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        if (!string.IsNullOrEmpty(sceneToLoadWhenDone))
        {
            SceneManager.LoadScene(sceneToLoadWhenDone);
        }
        else
        {
            SceneManager.UnloadSceneAsync("CutSceneLayer");
        }
    }
}
