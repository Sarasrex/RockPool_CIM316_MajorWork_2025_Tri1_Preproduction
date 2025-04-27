using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePanel : MonoBehaviour
{
    public GameObject panelToActivate;

    public void ShowPanel()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No panel assigned to ActivatePanel script.");
        }
    }
}
