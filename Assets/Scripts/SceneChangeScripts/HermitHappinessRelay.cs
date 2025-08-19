using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HermitHappinessRelay : MonoBehaviour
{
    public Hermit who;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnChanged);
    }

    void Start()
    {
        // initialise from stored value
        var gp = GameProgress.Instance;
        if (gp != null) slider.value = gp.Hermits[who].happiness;
    }

    void OnChanged(float v)
    {
        var gp = GameProgress.Instance;
        if (gp != null) gp.SetHappiness(who, v);
    }
}
