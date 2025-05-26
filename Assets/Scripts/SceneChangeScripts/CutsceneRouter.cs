using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneRouter : MonoBehaviour
{
    public static string cutsceneID = "Intro"; // Set this before loading the CutSceneLayer

    public GameObject cutscene_Intro;
    public GameObject cutscene_CaptainClaw;
    public GameObject cutscene_Pearl;
    public GameObject cutscene_Finn;
    public GameObject cutscene_Hans;
    public GameObject cutscene_Priscilla;
    public GameObject finalCutscene;

    void Start()
    {
        cutscene_Intro.SetActive(false);
        cutscene_CaptainClaw.SetActive(false);
        cutscene_Pearl.SetActive(false);
        cutscene_Finn.SetActive(false);
        cutscene_Hans.SetActive(false);
        cutscene_Priscilla.SetActive(false);
        finalCutscene.SetActive(false);

        switch (cutsceneID)
        {
            case "Intro": cutscene_Intro.SetActive(true); break;
            case "CaptainClaw": cutscene_CaptainClaw.SetActive(true); break;
            case "Pearl": cutscene_Pearl.SetActive(true); break;
            case "Finn": cutscene_Finn.SetActive(true); break;
            case "Hans": cutscene_Hans.SetActive(true); break;
            case "Priscilla": cutscene_Priscilla.SetActive(true); break;
            case "Final": finalCutscene.SetActive(true); break;
        }
    }
}
