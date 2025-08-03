using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonAudioToggle : MonoBehaviour
{
    [Header("Target Panel")]
    public GameObject panelToCheck;

    [Header("Audio Sources")]
    public AudioSource openAudio;
    public AudioSource closeAudio;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayToggleAudio);
    }

    void PlayToggleAudio()
    {
        if (panelToCheck == null) return;

        bool isCurrentlyActive = panelToCheck.activeSelf;

        if (isCurrentlyActive && closeAudio != null)
        {
            closeAudio.Play();
        }
        else if (!isCurrentlyActive && openAudio != null)
        {
            openAudio.Play();
        }
    }
}
