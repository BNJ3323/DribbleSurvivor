using UnityEngine;

enum PlayerState
{
    Running,
    PerformingMove,
    Stunned,
    BallLost,
    Exhausted,
    Finished
}

public class PlayerCharacter : MonoBehaviour
{
    private PlayerState state;

    public void Move(Vector2 direction)
    {
        
    }

    public void UseTechnicalMove(TechnicalMove move)
    {
        
    }

    public void LoseBall()
    {
        
    }

    public void CollectResource(Resources resource)
    {
        
    }

    public void UpdateStats()
    {
        
    }
}
