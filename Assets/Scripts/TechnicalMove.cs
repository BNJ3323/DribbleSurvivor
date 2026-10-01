using System.Collections.Generic;
using UnityEngine;

enum InputType
{
    Keyboard,
    Gamepad,
    Mouse
}

public class TechnicalMove : MonoBehaviour
{
    private string moveName;
    private int starLevel;
    private int enduranceCost;
    private float cooldown;
    
    private InputType inputType;
    private List<DefenderType> effectiveAgainst = new List<DefenderType>();

    public bool CanBeUsed(PlayerStats stats)
    {
        return true;
    }

    public void Execute(PlayerCharacter player)
    {
        
    }

    public bool IsEffectiveAgainst(Defenders defenders)
    {
        return true;
    }
}
