using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CutsceneTracker
{
    public static bool HasSeen(string id)
    {
        return UnityEngine.PlayerPrefs.GetInt("CutsceneSeen_" + id, 0) == 1;
    }

    public static void MarkSeen(string id)
    {
        UnityEngine.PlayerPrefs.SetInt("CutsceneSeen_" + id, 1);
    }
}
