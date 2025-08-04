using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 4)] public string text;
    public AudioSource audioSource; // updated from AudioClip
    public string animationTrigger;
}

[System.Serializable]
public class DialogueGroup
{
    public DialogueTriggerType triggerType;
    public DialogueLine[] lines;
}
