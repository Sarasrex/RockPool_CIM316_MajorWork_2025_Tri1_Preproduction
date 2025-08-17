using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// Lives in CutSceneLayer. Enables exactly one cutscene root based on CutsceneRequest.
/// When the cutscene completes, I mark it as seen and return to the requested scene via SceneSwitcher.
public class CutsceneRouter : MonoBehaviour
{
    public static CutsceneRouter Instance { get; private set; }

    [Header("Cutscene roots (set in inspector)")]
    public GameObject cutscene_Intro;
    public GameObject cutscene_CaptainClaw;
    public GameObject cutscene_Pearl;
    public GameObject cutscene_Finn;
    public GameObject cutscene_Hans;
    public GameObject cutscene_Priscilla;
    public GameObject cutscene_Final;

    [Header("Debug")]
    public bool verboseLogs = true;

    private string currentID;
    private string returnScene;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        DisableAll();

        currentID = string.IsNullOrEmpty(CutsceneRequest.CutsceneID) ? "Intro" : CutsceneRequest.CutsceneID;
        returnScene = CutsceneRequest.ReturnSceneName;

        if (verboseLogs)
            Debug.Log($"[CutsceneRouter] id={currentID} return={returnScene} force={CutsceneRequest.ForcePlay}");

        // Skip if I've already seen it and I'm not forcing playback
        if (!CutsceneRequest.ForcePlay && CutsceneTracker.HasSeen(currentID))
        {
            if (verboseLogs) Debug.Log("[CutsceneRouter] Already seen. Skipping…");
            FinishAndReturn(skipOnly: true);
            return;
        }

        // Activate the chosen cutscene root
        var go = FindRootForID(currentID);
        if (go == null)
        {
            Debug.LogError($"[CutsceneRouter] No cutscene root found for ID '{currentID}'.");
            FinishAndReturn(skipOnly: true);
            return;
        }

        go.SetActive(true);
    }

    private void DisableAll()
    {
        if (cutscene_Intro) cutscene_Intro.SetActive(false);
        if (cutscene_CaptainClaw) cutscene_CaptainClaw.SetActive(false);
        if (cutscene_Pearl) cutscene_Pearl.SetActive(false);
        if (cutscene_Finn) cutscene_Finn.SetActive(false);
        if (cutscene_Hans) cutscene_Hans.SetActive(false);
        if (cutscene_Priscilla) cutscene_Priscilla.SetActive(false);
        if (cutscene_Final) cutscene_Final.SetActive(false);
    }

    private GameObject FindRootForID(string id)
    {
        switch (id)
        {
            case "Intro": return cutscene_Intro;
            case "CaptainClaw": return cutscene_CaptainClaw;
            case "Pearl": return cutscene_Pearl;
            case "Finn": return cutscene_Finn;
            case "Hans": return cutscene_Hans;
            case "Priscilla": return cutscene_Priscilla;
            case "Final": return cutscene_Final;
            default: return null;
        }
    }

    /// I call this from the last panel/button when the cutscene finishes.
    public void CompleteCutscene()
    {
        if (!string.IsNullOrEmpty(currentID))
            CutsceneTracker.MarkSeen(currentID);

        FinishAndReturn(skipOnly: false);
    }

    private void FinishAndReturn(bool skipOnly)
    {
        if (string.IsNullOrEmpty(returnScene))
        {
            if (verboseLogs)
                Debug.LogWarning($"[CutsceneRouter] No return scene set. Staying put. (skipOnly={skipOnly})");
            return;
        }

        SceneSwitcher.Load(returnScene);
    }
}
