using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Hermit { CaptainClaw, Pearl, Finn, Hans }

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance { get; private set; }

    [Serializable]
    public class HermitState
    {
        public bool unlocked;
        public float happiness;   // 0–100
    }

    public Dictionary<Hermit, HermitState> Hermits = new Dictionary<Hermit, HermitState>()
    {
        { Hermit.CaptainClaw, new HermitState { unlocked = true,  happiness = 0 }}, // show only him first
        { Hermit.Pearl,       new HermitState { unlocked = false, happiness = 0 }},
        { Hermit.Finn,        new HermitState { unlocked = false, happiness = 0 }},
        { Hermit.Hans,        new HermitState { unlocked = false, happiness = 0 }},
    };

    [Range(0, 100)] public float gateThreshold = 50f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetHappiness(Hermit who, float value01to100)
    {
        var s = Hermits[who];
        s.happiness = Mathf.Clamp(value01to100, 0f, 100f);
        TryTriggerNextCutscene(who);
    }

    public void Unlock(Hermit who) => Hermits[who].unlocked = true;

    public static bool TryGetNext(Hermit current, out Hermit next)
    {
        switch (current)
        {
            case Hermit.CaptainClaw: next = Hermit.Pearl; return true;
            case Hermit.Pearl: next = Hermit.Finn; return true;
            case Hermit.Finn: next = Hermit.Hans; return true;
            default: next = default; return false;
        }
    }

    void TryTriggerNextCutscene(Hermit current)
    {
        if (!TryGetNext(current, out var next)) return;
        var cur = Hermits[current];
        var nxt = Hermits[next];

        if (cur.happiness >= gateThreshold && !nxt.unlocked)
        {
            // Ask to play the next hermit's cutscene, then return to the current scene
            string returnScene = SceneManager.GetActiveScene().name;

            CutsceneRequest.Set(CutsceneIdFor(next), returnScene, false);
            CutsceneRequest.NextUnlockHermit = next.ToString();   // tiny patch (see below)

            SceneSwitcher.Load("CutSceneLayer");
        }
    }

    public static string CutsceneIdFor(Hermit h)
    {
        // Matches your CutsceneRouter IDs
        switch (h)
        {
            case Hermit.Pearl: return "Pearl";
            case Hermit.Finn: return "Finn";
            case Hermit.Hans: return "Hans";
            default: return "";
        }
    }

    public static bool TryParseHermit(string s, out Hermit h)
    {
        foreach (Hermit val in Enum.GetValues(typeof(Hermit)))
            if (string.Equals(val.ToString(), s, StringComparison.OrdinalIgnoreCase)) { h = val; return true; }
        h = default; return false;
    }
}
