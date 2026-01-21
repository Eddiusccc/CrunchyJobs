using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager instance;
    public Worker[] allWorkers;
    float workTickTotal = 2f;
    float workTickCurrent = 0f;

    

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        allWorkers = GameObject.FindObjectsByType<Worker>(FindObjectsSortMode.None);
    }
    public Worker GetFreeWorker()
    {
        foreach (Worker worker in allWorkers)
        {
            if (worker.currentState == WorkerState.Descansando)
            {
                return worker;
            }
        }
        return null;
    }

    public void WorkerTick()
    {
        if (workTickCurrent < workTickTotal)
        {
            workTickCurrent += Time.deltaTime;
            return;
        }
        workTickCurrent = 0f;
        foreach (Worker worker in allWorkers)
        {
            if (worker.currentState == WorkerState.Descansando)
            {
                worker.RestApply();
            }
        }
    }

}
