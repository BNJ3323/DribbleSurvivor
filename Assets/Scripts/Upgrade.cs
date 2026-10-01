using UnityEngine;

enum UpgradeType
{
    OverallRating,
    Endurance,
    Speed,
    TechnicalMove
}

public class Upgrade : MonoBehaviour
{
    private string upgradeName;
    private UpgradeType upgradeType;
    private int costMoney;
    private int costAura;
    private int value;

    public bool CanBuy(PlayerStats stats)
    {
        return true;
    }

    public void Apply(PlayerStats stats)
    {
        
    }
}
