using UnityEngine;

enum GameState 
{
    MainMenu,
    Playing,
    Paused,
    Defeat,
    Hub,
    Preparation,
    HalfTime,
    Victory
}

public class GameManager : MonoBehaviour
{
    private GameState gameState;
    private float runDuration;
    private float halfTimeAt;

    public void StartRun()
    {
        
    }

    public void PauseRun()
    {
        
    }
    
    public void ResumeRun()
    {
        
    }
    
    public void RestartRun()
    {
        
    }

    public void EndRun()
    {
        
    }

    public void ReachHalfTime()
    {
        
    }
}
