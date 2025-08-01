using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMunchTest : MonoBehaviour
{
    public HermitReactionController testReaction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Press 'M' to test
        {
            if (testReaction != null)
                testReaction.PlayReaction("Munch");
        }
    }
}
