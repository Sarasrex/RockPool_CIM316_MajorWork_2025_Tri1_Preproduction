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
        { Hermit.CaptainClaw, new HermitState { unlocked = true,  happiness = 0 }}, // start with only Claw
        { Hermit.Pearl,       new HermitState { unlocked = false, happiness = 0 }},
        { Hermit.Finn,        new HermitState { unlocked = false, happiness = 0 }},
        { Hermit.Hans,        new HermitState { unlocked = false, happiness = 0 }},
    };

    [Range(0, 100)] public float gateThreshold = 50f;

    private bool finalPlayed = false;

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
        TryTriggerFinalCutscene();
    }

    public void Unlock(Hermit who) => Hermits[who].unlocked = true;

    // --- NEW HELPERS ---

    // Are ALL currently unlocked hermits at or above the given threshold?
    private bool AreAllUnlockedAtOrAbove(float threshold)
    {
        foreach (var kv in Hermits)
        {
            var state = kv.Value;
            if (!state.unlocked) continue;            // gate only on hermits visible/active
            if (state.happiness < threshold) return false;
        }
        return true;
    }

    // Who is the next hermit in the chain that is still locked?
    private bool TryGetFirstLocked(out Hermit next)
    {
        if (!Hermits[Hermit.Pearl].unlocked) { next = Hermit.Pearl; return true; }
        if (!Hermits[Hermit.Finn].unlocked) { next = Hermit.Finn; return true; }
        if (!Hermits[Hermit.Hans].unlocked) { next = Hermit.Hans; return true; }
        next = default; return false;
    }

    // (You can keep this if used elsewhere; unused by the new gate.)
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

    // --- REPLACED: gate = "all currently unlocked at ? threshold" ---
    void TryTriggerNextCutscene(Hermit _)
    {
        // Only progress if ALL currently unlocked hermits have reached the threshold
        if (!AreAllUnlockedAtOrAbove(gateThreshold)) return;

        // Find the next hermit in sequence who is still locked
        if (!TryGetFirstLocked(out var next)) return; // none left to unlock

        string returnScene = SceneManager.GetActiveScene().name;
        string id = CutsceneIdFor(next);

        // Play once per device/profile
        bool forcePlay = !CutsceneTracker.HasSeen(id);
        CutsceneRequest.Set(id, returnScene, forcePlay);

        // Tell the router which hermit to unlock on return
        CutsceneRequest.NextUnlockHermit = next.ToString();

        // If you're using additive overlay, swap to additive load here.
        SceneSwitcher.Load("CutSceneLayer");
    }

    public static string CutsceneIdFor(Hermit h)
    {
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

    public void ResetProgress()
    {
        // Exhibition reset: only Claw unlocked, everyone at 0, final not played
        Hermits[Hermit.CaptainClaw].unlocked = true;
        Hermits[Hermit.CaptainClaw].happiness = 0;

        Hermits[Hermit.Pearl].unlocked = false;
        Hermits[Hermit.Pearl].happiness = 0;

        Hermits[Hermit.Finn].unlocked = false;
        Hermits[Hermit.Finn].happiness = 0;

        Hermits[Hermit.Hans].unlocked = false;
        Hermits[Hermit.Hans].happiness = 0;

        finalPlayed = false;

        Debug.Log("[GameProgress] Progress reset.");
    }

    private void TryTriggerFinalCutscene()
    {
        if (finalPlayed) return;

        bool allFull =
            Hermits[Hermit.CaptainClaw].happiness >= 100f &&
            Hermits[Hermit.Pearl].happiness >= 100f &&
            Hermits[Hermit.Finn].happiness >= 100f &&
            Hermits[Hermit.Hans].happiness >= 100f;

        if (!allFull) return;

        string returnScene = SceneManager.GetActiveScene().name;
        bool forcePlay = !CutsceneTracker.HasSeen("Final"); // play once per device/profile

        CutsceneRequest.Set("Final", returnScene, forcePlay);

        // If using additive overlays, switch to additive load here.
        SceneSwitcher.Load("CutSceneLayer");

        finalPlayed = true;
        Debug.Log("[GameProgress] Final cutscene requested.");
    }
}
