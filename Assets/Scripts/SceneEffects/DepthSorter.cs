using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/*
DepthSorter.cs

My notes:
- I致e added a Sorting Group on the crab root so body + shell render as one unit.
- This script sits on that same root and sets the Sorting Group痴 order every frame.
- I知 using Z by default because I知 on a perspective camera and my crabs move on Z.
  (If I swap to top-down/orthographic, I can flip to Y sorting.)
- Lower on screen / closer to camera should draw in front, so I invert the value.
*/

[RequireComponent(typeof(SortingGroup))]
public class DepthSorter : MonoBehaviour
{
    // If I知 on a perspective camera and crabs move forward/back, use Z.
    // If I知 faking depth in 2D with vertical movement, use Y.
    public enum Axis { Y, Z }
    [Tooltip("Z for perspective depth (my default). Y for top-down/2D-style depth.")]
    public Axis axis = Axis.Z;

    [Tooltip("How strongly position affects draw order. 100-1000 works well.")]
    public int multiplier = 300;

    [Tooltip("Global bias if I need to push a crab in front/behind others.")]
    public int baseOrder = 0;

    private SortingGroup group;

    void Awake()
    {
        group = GetComponent<SortingGroup>();
        // My Sorting Group is left at Default layer, order 0 in the Inspector.
        // I知 not ticking 'Sort At Root'.
    }

    void LateUpdate()
    {
        // Grab the value I知 sorting by
        float v = (axis == Axis.Z) ? transform.position.z : transform.position.y;

        // Closer/lower should be on top -> invert and scale to an int
        int order = baseOrder - Mathf.RoundToInt(v * multiplier);

        // Apply to the whole crab (keeps shell + body stuck together visually)
        group.sortingOrder = order;
    }
}
