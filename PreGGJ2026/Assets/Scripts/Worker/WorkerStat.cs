using System;
using UnityEngine;

[Serializable]
public class WorkerStat
{
    [HideInInspector] public string statName;
    int currentValue;
    public StatType stat;
    public int maxValue;
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
    Resistencia,
    Estres,
    Velocidad,
    Comercio,
    Operaciones,
    Finanzas
}