using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntryGuard : MonoBehaviour
{
    void Awake()
    {
        // Always resume normal time & cursor when this scene loads
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
