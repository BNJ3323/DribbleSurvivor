using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    private List<Upgrade> availableUpgrades = new List<Upgrade>();

    public bool BuyUpgrade(Upgrade upgrade, PlayerStats stats)
    {
        return true;
    }
    
    public void ApplyUpgrade(Upgrade upgrade, PlayerStats stats)
    {
        
    }
}
