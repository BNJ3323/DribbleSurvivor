using UnityEngine;

public enum QualityLevel
{
    Low,
    Medium,
    High,
    UltraHigh
}

public enum Resolution
{
    Low,
    Medium,
    High,
    Custom
}

public class SettingsManager : MonoBehaviour
{
    private float masterVolume;
    private float musicVolume;
    private float sfxVolume;
    private Resolution resolution;
    private bool fullscreen;
    private QualityLevel quality;

    public void SetMasterVolume(float value)
    {
        
    }
    
    public void SetMusicVolume(float value)
    {
        
    }
    
    public void SetSFXVolume(float value)
    {
        
    }

    public void SetResolution(Resolution value)
    {
        
    }

    public void SetQuality(QualityLevel value)
    {
        
    }
}
