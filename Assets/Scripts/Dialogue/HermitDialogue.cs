using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitDialogue : MonoBehaviour
{
    public DialogueGroup[] dialogueGroups;

    public DialogueLine GetRandomLineByTrigger(DialogueTriggerType trigger)
    {
        foreach (DialogueGroup group in dialogueGroups)
        {
            if (group.triggerType == trigger && group.lines.Length > 0)
            {
                return group.lines[Random.Range(0, group.lines.Length)];
            }
        }

        return new DialogueLine { text = "..." };
    }
}

// Need to add a randomised general state trigger after 30 seconds of no input. TBC