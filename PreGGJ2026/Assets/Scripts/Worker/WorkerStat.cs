using System;
using UnityEngine;

[Serializable]
public class WorkerStat
{
    [HideInInspector] public string statName;
    int currentValue;
    public StatType stat;
    public int maxValue;

    public int CurrentValue()
    {
        return currentValue;
    }
    public void SetCurrentValue(int value)
    {
        currentValue = Mathf.Clamp(value, 0, maxValue);
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