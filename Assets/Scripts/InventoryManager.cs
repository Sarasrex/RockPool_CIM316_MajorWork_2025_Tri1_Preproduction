using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Track homes as treasures
    public bool hasBaseHome = true;  // Starting home
    public bool hasGoldCoinHome = false;
    public bool hasMirrorShellHome = false;
    public bool hasBoardNose = false;
    public bool hasRockBowl = false;
    public bool hasSnorkel = false;
    public bool hasShellPurse = false;
    public bool hasLostCompass = false;
    public bool hasCharStick = false;
    public bool hasCoralSpoon = false;
    public bool hasStoneStack = false;
    public bool hasGlassHeart = false;


    // Track food items
    public bool hasSeaweedSnarlRoll = false;
    public bool hasSquidlyChew = false;
    public bool hasDriftChipDip = false;
    public bool hasSeaSalad = false;
    public bool hasJellyWiggle = false;
    public bool hasBubbleBurst = false;
    public bool hasBeachBanana = false;
    public bool hasBubbleBounce = false;
    public bool hasNoriNibble = false;
    public bool hasSnailSlurp = false;
    public bool hasPlumDrop = false;
    public bool hasStars = false;


    // Defining constants for treasures and food items
    public const string GoldCoinHome = "GoldCoinHome";
    public const string MirrorShellHome = "MirrorShellHome";
    public const string BoardNose = "BoardNose";
    public const string RockBowl = "RockBowl";
    public const string Snorkel = "Snorkel";
    public const string ShellPurse = "ShellPurse";
    public const string LostCompass = "LostCompass";
    public const string CharStick = "CharStick";
    public const string CoralSpoon = "CoralSpoon";
    public const string StoneStack = "StoneStack";
    public const string GlassHeart = "GlassHeart";

    // Food items
    public const string SeaweedSnarlRoll = "SeaweedSnarlRoll";
    public const string SquidlyChew = "SquidlyChew";
    public const string DriftChipDip = "DriftChipDip";
    public const string SeaSalad = "SeaSalad";
    public const string JellyWiggle = "JellyWiggle";
    public const string BubbleBurst = "BubbleBurst";
    public const string BeachBanana = "BeachBanana";
    public const string CrunchMunch = "CrunchMunch";
    public const string BubbleBounce = "BubbleBounce";
    public const string NoriNibble = "NoriNibble";
    public const string SnailSlurp = "SnailSlurp";
    public const string PlumDrop = "PlumDrop";
    public const string Stars = "Stars";


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Collect an item
    public void CollectItem(string itemName, string itemCategory)
    {
        switch (itemCategory)
        {
            // Homes (upgrade)
            case "Home":
                if (itemName == GoldCoinHome) hasGoldCoinHome = true;
                if (itemName == MirrorShellHome) hasMirrorShellHome = true;
                break;

            // Food
            case "Food":
                if (itemName == SeaweedSnarlRoll) hasSeaweedSnarlRoll = true;
                if (itemName == SquidlyChew) hasSquidlyChew = true;
                if (itemName == DriftChipDip) hasDriftChipDip = true;
                if (itemName == SeaSalad) hasSeaSalad = true;
                if (itemName == JellyWiggle) hasJellyWiggle = true;
                if (itemName == BubbleBurst) hasBubbleBurst = true;
                if (itemName == BeachBanana) hasBeachBanana = true;
                if (itemName == BubbleBounce) hasBubbleBounce = true;
                if (itemName == NoriNibble) hasNoriNibble = true;
                if (itemName == SnailSlurp) hasSnailSlurp = true;
                if (itemName == PlumDrop) hasPlumDrop = true;
                if (itemName == Stars) hasStars = true;
                break;

            // Treasures (non-food items)
            case "Treasure":
                if (itemName == BoardNose) hasBoardNose = true;
                if (itemName == RockBowl) hasRockBowl = true;
                if (itemName == Snorkel) hasSnorkel = true;
                if (itemName == ShellPurse) hasShellPurse = true;
                if (itemName == LostCompass) hasLostCompass = true;
                if (itemName == CharStick) hasCharStick = true;
                if (itemName == CoralSpoon) hasCoralSpoon = true;
                if (itemName == StoneStack) hasStoneStack = true;
                if (itemName == GlassHeart) hasGlassHeart = true;
                break;
        }

        // Update UI
        UIManager.Instance.ShowItem(itemName, itemCategory);
    }
}
