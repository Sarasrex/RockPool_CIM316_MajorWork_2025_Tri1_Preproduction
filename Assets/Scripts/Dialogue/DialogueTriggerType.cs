using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueTriggerType
{
    Hello,
    InAgreement,
    Disapproves,
    Munching,
    Sleeping,
    GeneralState
}

// Need to change to something mroe liek this to fit design:

/*
public enum DialogueTriggerType
 {
    Hello,
    LikesFood,
    DislikesFood,
    NeutralFood,
    LikesHome,
    DislikesHome,
    Sleeping,
    GeneralState
}

// This will go into my Recieve Item method. TBC

DialogueTriggerType trigger;

if (itemCategory == "Food")
{
    if (System.Array.Exists(likedFoods, item => item == itemName))
        trigger = DialogueTriggerType.LikesFood;
    else if (System.Array.Exists(dislikedFoods, item => item == itemName))
        trigger = DialogueTriggerType.DislikesFood;
    else
        trigger = DialogueTriggerType.NeutralFood;
}
else if (itemCategory == "Home")
{
    if (System.Array.Exists(likedHomes, item => item == itemName))
        trigger = DialogueTriggerType.LikesHome;
    else if (System.Array.Exists(dislikedHomes, item => item == itemName))
        trigger = DialogueTriggerType.DislikesHome;
    else
        trigger = DialogueTriggerType.GeneralState;
}
else
{
    trigger = DialogueTriggerType.GeneralState;
}


*/