using System;
using UnityEngine;

[Serializable]
public class WorkerStats
{
    [SerializeField] public int maxValue;
    public EnumWorkerStat stat;
    [HideInInspector] public int currentValue;


    public void CambiarStatRandom(int limiteMin, int limiteMax)
    {
        int rng = UnityEngine.Random.Range(limiteMin, limiteMax + 1);
        maxValue = rng;
        currentValue = rng;
    }

   
}


public enum EnumWorkerStat
{
    Cansancio, Trabajo, Estres, Computacion, Papeleo, Comunicacion
}
