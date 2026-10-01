using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, HalfTime, Paused, Victory, Defeat }

    [Header("Références")]
    [SerializeField] private RunManager runManager;
    [SerializeField] private UIManager uiManager;

    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public bool IsPlaying => CurrentState == GameState.Playing;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    
    private void Start()
    {
        StartRun();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing) PauseRun();
            else if (CurrentState == GameState.Paused) ResumeRun();
        }
    }

    public void StartRun()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        uiManager?.ShowGameplay();
        runManager?.StartStage();
    }

    public void ReachHalfTime()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.HalfTime;
        Time.timeScale = 0f;
        uiManager?.ShowHalfTime();
    }

    public void ResumeAfterHalfTime()
    {
        if (CurrentState != GameState.HalfTime) return;
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        uiManager?.ShowGameplay();
    }

    public void PauseRun()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        uiManager?.ShowPause();
    }

    public void ResumeRun()
    {
        if (CurrentState != GameState.Paused) return;
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        uiManager?.ShowGameplay();
    }

    public void WinRun()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Victory;
        Time.timeScale = 0f;
        uiManager?.ShowVictory();
    }

    public void LoseRun()
    {
        if (CurrentState == GameState.Defeat || CurrentState == GameState.Victory) return;
        CurrentState = GameState.Defeat;
        Time.timeScale = 0f;
        uiManager?.ShowDefeat();
    }

    public void RestartRun()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        uiManager?.ShowGameplay();
        runManager?.RestartStage();
    }
}