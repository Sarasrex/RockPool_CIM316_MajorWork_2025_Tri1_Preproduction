using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HermitHomeBinder : MonoBehaviour
{
    [Header("Crab roots (enable/disable)")]
    public GameObject captainClawRoot;
    public GameObject pearlRoot;
    public GameObject finnRoot;
    public GameObject hansRoot;

    [Header("Happiness sliders (0..100)")]
    public Slider captainClawSlider;
    public Slider pearlSlider;
    public Slider finnSlider;
    public Slider hansSlider;

    void Start() { Refresh(); }

    void OnEnable() { Refresh(); }

    public void Refresh()
    {
        var gp = GameProgress.Instance;
        if (gp == null) return;

        SetActiveAndValue(captainClawRoot, captainClawSlider, gp.Hermits[Hermit.CaptainClaw]);
        SetActiveAndValue(pearlRoot, pearlSlider, gp.Hermits[Hermit.Pearl]);
        SetActiveAndValue(finnRoot, finnSlider, gp.Hermits[Hermit.Finn]);
        SetActiveAndValue(hansRoot, hansSlider, gp.Hermits[Hermit.Hans]);
    }

    void SetActiveAndValue(GameObject root, Slider slider, GameProgress.HermitState state)
    {
        if (root) root.SetActive(state.unlocked);
        if (slider) slider.value = state.happiness;
    }
}
