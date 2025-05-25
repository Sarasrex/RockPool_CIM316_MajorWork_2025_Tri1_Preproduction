using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Track homes as treasures
    public bool hasBaseHome = true;  // Starting home
    public bool hasGoldCoinHome = false;
    public bool hasMirrorShellHome = false;
    public bool hasBoardNoseHome = false;
    public bool hasRockBowlHome = false;
    public bool hasSnorkelHome = false;
    public bool hasShellPurseHome = false;
    public bool hasLostCompassHome = false;
    public bool hasCharStickHome = false;
    public bool hasCoralSpoonHome = false;
    public bool hasStoneStackHome = false;
    public bool hasGlassHeartHome = false;
    public bool hasUsbStickHome = false;


    // Track food items
    public bool hasSeaweedSnarlRoll = false;
    public bool hasSquidlyChew = false;
    public bool hasDriftChipDip = false;
    public bool hasSeaSalad = false;
    public bool hasJellyWiggle = false;
    public bool hasBubbleBurst = false;
    public bool hasBeachBanana = false;
    public bool hasCrunchMunch = false;
    public bool hasBubbleBounce = false;
    public bool hasNoriNibble = false;
    public bool hasSnailSlurp = false;
    public bool hasPlumDrop = false;
    public bool hasStars = false;


    // Defining constants for treasures and food items
    public const string GoldCoinHome = "GoldCoinHome";
    public const string MirrorShellHome = "MirrorShellHome";
    public const string BoardNoseHome = "BoardNoseHome";
    public const string RockBowlHome = "RockBowlHome";
    public const string SnorkelHome = "SnorkelHome";
    public const string ShellPurseHome = "ShellPurseHome";
    public const string LostCompassHome = "LostCompassHome";
    public const string CharStickHome = "CharStickHome";
    public const string CoralSpoonHome = "CoralSpoonHome";
    public const string StoneStackHome = "StoneStackHome";
    public const string GlassHeartHome = "GlassHeartHome";
    public const string UsbStickHome = "UsbStickHome";

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

    public Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Collect an item
    public void CollectItem(string itemName, string itemCategory)
    {
        // Count tracking
        if (itemCounts.ContainsKey(itemName))
            itemCounts[itemName]++;
        else
            itemCounts[itemName] = 1;

        // Boolean tracking for compatibility
        switch (itemCategory)
        {
            case "Home":
                if (itemName == GoldCoinHome) hasGoldCoinHome = true;
                if (itemName == MirrorShellHome) hasMirrorShellHome = true;
                if (itemName == BoardNoseHome) hasBoardNoseHome = true;
                if (itemName == RockBowlHome) hasRockBowlHome = true;
                if (itemName == SnorkelHome) hasSnorkelHome = true;
                if (itemName == ShellPurseHome) hasShellPurseHome = true;
                if (itemName == LostCompassHome) hasLostCompassHome = true;
                if (itemName == CharStickHome) hasCharStickHome = true;
                if (itemName == CoralSpoonHome) hasCoralSpoonHome = true;
                if (itemName == StoneStackHome) hasStoneStackHome = true;
                if (itemName == GlassHeartHome) hasGlassHeartHome = true;
                if (itemName == UsbStickHome) hasUsbStickHome = true;
                break;

            case "Food":
                if (itemName == SeaweedSnarlRoll) hasSeaweedSnarlRoll = true;
                if (itemName == SquidlyChew) hasSquidlyChew = true;
                if (itemName == DriftChipDip) hasDriftChipDip = true;
                if (itemName == SeaSalad) hasSeaSalad = true;
                if (itemName == JellyWiggle) hasJellyWiggle = true;
                if (itemName == BubbleBurst) hasBubbleBurst = true;
                if (itemName == BeachBanana) hasBeachBanana = true;
                if (itemName == CrunchMunch) hasCrunchMunch = true;
                if (itemName == BubbleBounce) hasBubbleBounce = true;
                if (itemName == NoriNibble) hasNoriNibble = true;
                if (itemName == SnailSlurp) hasSnailSlurp = true;
                if (itemName == PlumDrop) hasPlumDrop = true;
                if (itemName == Stars) hasStars = true;
                break;
        }

        // Update UI and pass the count
        UIManager.Instance.ShowItem(itemName, itemCategory, itemCounts[itemName]);
    }

    public void UseItem(string itemName, string itemCategory)
    {
        if (itemCounts.ContainsKey(itemName) && itemCounts[itemName] > 0)
        {
            itemCounts[itemName]--;

            // Optional: set boolean to false if count is now 0
            if (itemCounts[itemName] == 0)
            {
                switch (itemCategory)
                {
                    case "Home":
                        if (itemName == GoldCoinHome) hasGoldCoinHome = false;
                        if (itemName == MirrorShellHome) hasMirrorShellHome = false;
                        if (itemName == BoardNoseHome) hasBoardNoseHome = false;
                        if (itemName == RockBowlHome) hasRockBowlHome = false;
                        if (itemName == SnorkelHome) hasSnorkelHome = false;
                        if (itemName == ShellPurseHome) hasShellPurseHome = false;
                        if (itemName == LostCompassHome) hasLostCompassHome = false;
                        if (itemName == CharStickHome) hasCharStickHome = false;
                        if (itemName == CoralSpoonHome) hasCoralSpoonHome = false;
                        if (itemName == StoneStackHome) hasStoneStackHome = false;
                        if (itemName == GlassHeartHome) hasGlassHeartHome = false;
                        if (itemName == UsbStickHome) hasUsbStickHome = false;
                        break;

                    case "Food":
                        if (itemName == SeaweedSnarlRoll) hasSeaweedSnarlRoll = false;
                        if (itemName == SquidlyChew) hasSquidlyChew = false;
                        if (itemName == DriftChipDip) hasDriftChipDip = false;
                        if (itemName == SeaSalad) hasSeaSalad = false;
                        if (itemName == JellyWiggle) hasJellyWiggle = false;
                        if (itemName == BubbleBurst) hasBubbleBurst = false;
                        if (itemName == BeachBanana) hasBeachBanana = false;
                        if (itemName == CrunchMunch) hasCrunchMunch = false;
                        if (itemName == BubbleBounce) hasBubbleBounce = false;
                        if (itemName == NoriNibble) hasNoriNibble = false;
                        if (itemName == SnailSlurp) hasSnailSlurp = false;
                        if (itemName == PlumDrop) hasPlumDrop = false;
                        if (itemName == Stars) hasStars = false;
                        break;
                }
            }

            // Update the UI
            UIManager.Instance.ShowItem(itemName, itemCategory, itemCounts[itemName]);
        }
        else
        {
            Debug.LogWarning($"Tried to use item '{itemName}' but none are available.");
        }
    }

}

