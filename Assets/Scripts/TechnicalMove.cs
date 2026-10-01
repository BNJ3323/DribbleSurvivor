using System.Collections.Generic;
using UnityEngine;

enum InputType
{
    Keyboard,
    Gamepad,
    Mouse
}

[CreateAssetMenu(fileName = "GT_Nouveau", menuName = "Dribble Survivor/Geste Technique")]
public class TechnicalMove : ScriptableObject
{
    [SerializeField] private string moveName = "Crochet";
    [SerializeField, Range(1, 5)] private int starLevel = 1;
    [SerializeField, Min(0)] private float enduranceCost = 10f;
    [SerializeField, Min(0)] private float cooldown = 0.4f;
    
    private InputType inputType;
    [SerializeField] private List<Defenders.DefenderType> effectiveAgainst = new List<Defenders.DefenderType>();
    
    public string MoveName => moveName;
    public int StarLevel => starLevel; 
    public float EnduranceCost => enduranceCost;
    public float Cooldown => cooldown;

    public bool CanBeUsed(PlayerStats stats)
    {
        return stats != null && stats.CurrentEndurance >= enduranceCost;;
    }

    public void Execute(PlayerCharacter player)
    {
        
    }

    public bool IsEffectiveAgainst(Defenders defenders)
    {
        if (defenders == null) return false;
        return effectiveAgainst.Count == 0 || effectiveAgainst.Contains(defenders.Type);
    }
}
