using System.Collections.Generic;
using UnityEngine;

public class Stage : World
{
    private int stageNumber;
    private int defenderLevel;
    private float duration;
    
    private List<Defenders> defenders = new List<Defenders>();

    public void GenerateDefenders()
    {
        
    }

    public bool IsCompleted()
    {
        return true;
    }

    public void ResetStage()
    {
        
    }
}
