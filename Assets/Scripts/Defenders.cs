using UnityEngine;

enum DefenderState
{
    Approaching,
    Challenging,
    Dribbled,
    Blocking,
    Defeated
}

enum DefenderType
{
    Basic,
    Fast,
    Strong,
    Technical,
    Elite,
    Boss
}

public class Defenders : Stage
{
    private int level;
    private DefenderState state;
    private DefenderType type;

    public void Approach(PlayerCharacter player)
    {
        
    }
    
    public void ReactToMove(TechnicalMove move)
    {
        
    }

    public void StopPlayer()
    {
        
    }

    public void ShowSuccessFeedback()
    {
        
    }
}
