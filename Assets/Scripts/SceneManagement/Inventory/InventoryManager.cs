using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Starting home
    public bool hasBaseHome = true;

    // Home items
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

    // Food items
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

    // Constants (clean references)
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

    // For saving collected items
    public static List<string> collectedItems = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem(string itemName, string itemCategory)
    {
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

        if (!collectedItems.Contains(itemName))
            collectedItems.Add(itemName);

        UIManager.Instance.ShowItem(itemName, itemCategory);
    }

    public void LoadInventory()
    {
        foreach (string item in collectedItems)
        {
            if (item.Contains("Home"))
                UIManager.Instance.ShowItem(item, "Home");
            else
                UIManager.Instance.ShowItem(item, "Food");
        }
    }
}
