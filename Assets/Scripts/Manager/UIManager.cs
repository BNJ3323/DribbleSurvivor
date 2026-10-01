using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private Slider enduranceBar;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI auraText;
    [SerializeField] private TextMeshProUGUI overallText;
    [Header("Panneaux")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject halfTimePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ShowGameplay();
    }

    public void UpdatePlayerStats(PlayerStats stats)
    {
        if (stats == null) return;
        if (enduranceBar != null) { enduranceBar.maxValue = stats.MaxEndurance; enduranceBar.value = stats.CurrentEndurance; }
        if (moneyText != null) moneyText.text = $"Argent : {stats.Money}";
        if (auraText != null) auraText.text = $"Aura : {stats.Aura}";
        if (overallText != null) overallText.text = $"NG : {stats.OverallRating}";
    }

    public void UpdateTimer(float remaining)
    {
        if (timerText == null) return;
        int seconds = Mathf.CeilToInt(remaining);
        timerText.text = $"{seconds / 60:00}:{seconds % 60:00}";
    }

    public void ShowGameplay() => SetPanels(true, false, false, false, false);
    public void ShowPause() => SetPanels(true, true, false, false, false);
    public void ShowHalfTime() => SetPanels(true, false, true, false, false);
    public void ShowVictory() => SetPanels(false, false, false, true, false);
    public void ShowDefeat() => SetPanels(false, false, false, false, true);

    public void Resume() => GameManager.Instance?.ResumeRun();
    public void ResumeHalfTime() => GameManager.Instance?.ResumeAfterHalfTime();
    public void Restart() => GameManager.Instance?.RestartRun();

    private void SetPanels(bool hud, bool pause, bool halfTime, bool victory, bool defeat)
    {
        if (gameplayPanel != null) gameplayPanel.SetActive(hud);
        if (pausePanel != null) pausePanel.SetActive(pause);
        if (halfTimePanel != null) halfTimePanel.SetActive(halfTime);
        if (victoryPanel != null) victoryPanel.SetActive(victory);
        if (defeatPanel != null) defeatPanel.SetActive(defeat);
    }
}