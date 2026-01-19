using System;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public WorkerStats[] stats;

    private void OnValidate()
    {
        SetStatsArray();
    }
    void SetStatsArray()
    {
        int enumCount = Enum.GetValues(typeof(EnumWorkerStat)).Length;
        stats = new WorkerStats[enumCount];

        for (int i = 0; i < enumCount; i++)
        {
            if (stats[i] == null)
                stats[i] = new WorkerStats();

            stats[i].stat = (EnumWorkerStat)i;
        }
    }

    public WorkerStats FindStat(EnumWorkerStat statToFind)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            {
            if (stats[i].stat == statToFind)
                return stats[i];
            }
        }
        Debug.LogWarning("Stat not found: " + statToFind);
        return null;
    }

    private void Start()
    {
        FindStat(EnumWorkerStat.Cansancio).maxValue = 10;
    }
}
