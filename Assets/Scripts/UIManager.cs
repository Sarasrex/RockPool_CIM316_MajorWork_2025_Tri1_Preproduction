using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // UI Elements for treasure (home) icons
    public GameObject goldCoinHomeIcon;
    public GameObject mirrorShellHomeIcon;
    public GameObject boardNoseIcon;
    public GameObject rockBowlIcon;
    public GameObject snorkelIcon;  
    public GameObject shellPurseIcon;  
    public GameObject lostCompassIcon;
    public GameObject charStickIcon;
    public GameObject coralSpoonIcon;
    public GameObject stoneStackIcon;
    public GameObject glassHeartIcon;

    // UI Elements for food icons
    public GameObject seaweedSnarlRollIcon;
    public GameObject squidlyChewIcon;
    public GameObject driftChipDipIcon;
    public GameObject seaSaladIcon;
    public GameObject jellyWiggleIcon;
    public GameObject bubbleBurstIcon;
    public GameObject beachBananaIcon;
    public GameObject crunchMunchIcon;
    public GameObject bubbleBounceIcon;
    public GameObject noriNibbleIcon;
    public GameObject snailSlurpIcon;
    public GameObject plumDropIcon;
    public GameObject starsIcon;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Show collected item icons based on category
    public void ShowItem(string itemName, string itemCategory)
    {
        switch (itemCategory)
        {
            case "Home":
                if (itemName == "GoldCoinHome") goldCoinHomeIcon.SetActive(true);
                if (itemName == "MirrorShellHome") mirrorShellHomeIcon.SetActive(true);
                if (itemName == "BoardNoseHome") boardNoseIcon.SetActive(true);
                if (itemName == "RockBowlHome") rockBowlIcon.SetActive(true);
                if (itemName == "SnorkelHome") snorkelIcon.SetActive(true);
                if (itemName == "ShellPurseHome") shellPurseIcon.SetActive(true);
                if (itemName == "LostCompassHome") lostCompassIcon.SetActive(true);
                if (itemName == "CharStickHome") charStickIcon.SetActive(true);
                if (itemName == "CoralSpoonHome") coralSpoonIcon.SetActive(true);
                if (itemName == "StoneStackHome") stoneStackIcon.SetActive(true);
                if (itemName == "GlassHeartHome") glassHeartIcon.SetActive(true);
                break;

            case "Food":
                if (itemName == "SeaweedSnarlRoll") seaweedSnarlRollIcon.SetActive(true);
                if (itemName == "SquidlyChew") squidlyChewIcon.SetActive(true);
                if (itemName == "DriftChipDip") driftChipDipIcon.SetActive(true);
                if (itemName == "SeaSalad") seaSaladIcon.SetActive(true);
                if (itemName == "JellyWiggle") jellyWiggleIcon.SetActive(true);
                if (itemName == "BubbleBurst") bubbleBurstIcon.SetActive(true);
                if (itemName == "BeachBanana") beachBananaIcon.SetActive(true);
                if (itemName == "CrunchMunch") crunchMunchIcon.SetActive(true);
                if (itemName == "BubbleBounce") bubbleBounceIcon.SetActive(true);
                if (itemName == "NoriNibble") noriNibbleIcon.SetActive(true);
                if (itemName == "SnailSlurp") snailSlurpIcon.SetActive(true);
                if (itemName == "PlumDrop") plumDropIcon.SetActive(true);
                if (itemName == "Stars") starsIcon.SetActive(true);
                break;
        }
    }
}
