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
    [HideInInspector] public Job currentJob;

    private void OnValidate()
    {
        SetupStats();
        SetRandomStats();
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
            workerStats[i].SetMaxValue(0);
            workerStats[i].SetCurrentValue(workerStats[i].maxValue);
        }
    }

    void SetRandomStats()
    {
        foreach (var stat in workerStats)
        {
            switch (stat.stat)
            {
                case StatType.Energia:
                    stat.SetMaxValue(UnityEngine.Random.Range(WorkerStat.minStatValue + 6, WorkerStat.maxStatValue));
                    stat.SetCurrentValue(stat.maxValue);
                    break;
                case StatType.Estres:
                    stat.SetMaxValue(UnityEngine.Random.Range(WorkerStat.maxStatValue - 7, WorkerStat.maxStatValue));
                    stat.SetCurrentValue(1);
                    break;
                case StatType.Velocidad:
                    stat.SetMaxValue(UnityEngine.Random.Range(WorkerStat.minStatValue, WorkerStat.maxStatValue));
                    stat.SetCurrentValue(stat.maxValue);
                    break;
                default:
                    stat.SetMaxValue(UnityEngine.Random.Range(WorkerStat.minStatValue, WorkerStat.maxStatValue));
                    stat.SetCurrentValue(stat.maxValue);
                    break;
            }
        }
    }

    #region MONITOR WORKER STATS

    public void WorkApply()
    {
        //Aplicar estres
        //Consumir energia

        float modifEstres = (float)GetWorkerStat(StatType.Estres).CurrentValue();
        modifEstres = modifEstres.Remap(WorkerStat.minStatValue, WorkerStat.maxStatValue, 1f, 2f);
        int energia = Mathf.RoundToInt(UnityEngine.Random.Range(1, 3) * modifEstres);
        int estres = Mathf.RoundToInt(UnityEngine.Random.Range(1, 4));

        DepleteEnergia(energia);
        DepleteEstres(estres);
    }
    void DepleteEstres(int estresToAdd)
    {
        WorkerStat estresStat = GetWorkerStat(StatType.Estres);
        estresStat.IncreaseCurrentValue(estresToAdd);
        CheckEstres();
    }
    void CheckEstres()
    {
        WorkerStat estresStat = GetWorkerStat(StatType.Estres);
        if (estresStat.CurrentValue() >= estresStat.maxValue)
        {
            currentState = WorkerState.NoDisponible;
            currentJob.ForceRemoveWorker();
        }
    }
    void DepleteEnergia(int energiaToDeplete)
    {
        WorkerStat energiaStat = GetWorkerStat(StatType.Energia);
        energiaStat.IncreaseCurrentValue(-energiaToDeplete);
        CheckEnergia();
    }
    void CheckEnergia()
    {
        WorkerStat energiaStat = GetWorkerStat(StatType.Energia);
        if (energiaStat.CurrentValue() <= WorkerStat.minStatValue)
        {
            currentState = WorkerState.NoDisponible;
            currentJob.ForceRemoveWorker();
        }
    }
    public void RestApply()
    {
        WorkerStat energiaStat = GetWorkerStat(StatType.Energia);
        energiaStat.IncreaseCurrentValue(UnityEngine.Random.Range(1, 3));
    }


    #endregion
}

public enum WorkerState
{
    Descansando,
    Trabajando,
    NoDisponible,
    None
}
