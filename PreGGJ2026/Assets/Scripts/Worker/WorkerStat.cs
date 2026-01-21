using System;
using UnityEngine;

[Serializable]
public class WorkerStat
{
    [HideInInspector] public string statName;
    public StatType stat;
    public int maxValue;
    public int currentValue;

    public static int maxStatValue = 20;
    public static int minStatValue = 1;
    public int CurrentValue()
    {
        return currentValue;
    }

    public void SetMaxValue(int value)
    {
        maxValue = Mathf.Clamp(value, minStatValue, maxStatValue);
    }
    public void IncreaseCurrentValue(int amount)
    {
        SetCurrentValue(currentValue + amount);
    }

    public void SetCurrentValue(int value)
    {
        currentValue = Mathf.Clamp(value, minStatValue, maxValue);
    }
    public float GetStatPercentage()
    {
        if (maxValue == 0) return 0f;
        return (float)currentValue / maxValue;
    }

}

public enum StatType
{
    Energia,
    Estres,
    Velocidad,
    Comercio,
    Operaciones,
    Finanzas
}