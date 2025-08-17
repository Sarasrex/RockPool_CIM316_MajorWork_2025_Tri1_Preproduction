using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Tiny PlayerPrefs helper so I can mark a cutscene as seen and skip it next time.
public static class CutsceneTracker
{
    public static bool HasSeen(string id)
    {
        return PlayerPrefs.GetInt("CutsceneSeen_" + id, 0) == 1;
    }

    public static void MarkSeen(string id)
    {
        PlayerPrefs.SetInt("CutsceneSeen_" + id, 1);
        PlayerPrefs.Save();
    }
}
