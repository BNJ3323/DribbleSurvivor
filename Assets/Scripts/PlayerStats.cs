using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    [SerializeField, Min(1)] private int overallRating = 10;
    [SerializeField, Min(1)] private int endurance = 1;
    [SerializeField, Min(1)] private int speed = 1;
    [SerializeField, Min(1)] private float maxEndurance = 100f;
    [SerializeField] private float currentEndurance = 100f;
    [SerializeField] private int money;
    [SerializeField] private int aura;

    public int OverallRating => overallRating;
    public int EnduranceLevel => endurance;
    public int SpeedLevel => speed;
    public float CurrentEndurance => currentEndurance;
    public float MaxEndurance => maxEndurance;
    public int Money => money;
    public int Aura => aura;
    public float EndurancePercent => maxEndurance <= 0 ? 0 : currentEndurance / maxEndurance;

    public event Action Changed;

    public void ResetForRun()
    {
        currentEndurance = maxEndurance;
        Changed?.Invoke();
    }

    public bool SpendEndurance(float amount)
    {
        if (amount <= 0) return true;
        if (currentEndurance < amount) return false;
        currentEndurance = Mathf.Max(0, currentEndurance - amount);
        Changed?.Invoke();
        return true;
    }

    public void RestoreEndurance(float amount)
    {
        currentEndurance = Mathf.Clamp(currentEndurance + amount, 0, maxEndurance);
        Changed?.Invoke();
    }

    public void AddMoney(int amount) { money += Mathf.Max(0, amount); Changed?.Invoke(); }
    public void AddAura(int amount) { aura += Mathf.Max(0, amount); Changed?.Invoke(); }

    public void IncreaseOverallRating()
    {
        overallRating++;
        endurance++;
        speed++;
        maxEndurance += 10f;
        currentEndurance = Mathf.Min(maxEndurance, currentEndurance + 10f);
        Changed?.Invoke();
    }

    public void IncreaseEndurance() { endurance++; maxEndurance += 15f; currentEndurance += 15f; Changed?.Invoke(); }
    public void IncreaseSpeed() { speed++; Changed?.Invoke(); }
}