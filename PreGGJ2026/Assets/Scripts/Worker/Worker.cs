using System;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [Header("WORKER DATA")]
    [Space(10)]
    public string workerName;
    // Hacer el sistema de portada del trabajador, juntando los sprites assets.
    public WorkerStat[] workerStats;
    public WorkerState currentState = WorkerState.Descansando;
    private void OnValidate()
    {
        SetupStats();
    }
    
    public WorkerStat GetWorkerStat(StatType statType)
    {
        foreach (WorkerStat stat in workerStats)
        {
            if (stat.stat == statType)
            {
                return stat;
            }
        }
        return null;
    }

    void SetupStats()
    {
        int enumLength = Enum.GetValues(typeof(StatType)).Length;
        workerStats = new WorkerStat[enumLength];
        for (int i = 0; i < enumLength; i++)
        {
            workerStats[i] = new WorkerStat();
            workerStats[i].stat = (StatType)i;
            workerStats[i].statName = workerStats[i].stat.ToString();
            workerStats[i].maxValue = 000;
            workerStats[i].SetCurrentValue(workerStats[i].maxValue);
        }
    }


    #region MONITOR WORKER STATS
    //Monitorear el estres, y la resistencia.
    #endregion
}

public enum WorkerState
{
    Descansando,
    Trabajando,
    NoDisponible,
    None
}
