using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private int worldNumber;
    private string displayName;
    private List<Stage> stages = new List<Stage>();

    public void UnlockNextWorld()
    {
        
    }

    public Stage GetStage(int index)
    {
        return stages[index];
    }
}
