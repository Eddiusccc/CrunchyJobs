using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    JobManager jobManager;
    WorkerManager workerManager;
    public int objectivesCompleted = 0;
    public int jobPointsTotal = 0;
    public int jobPointsCurrent = 0;
    public int level = 1;

    [InspectorButton("OnButtonClicked")]
    public bool test;

    private void OnButtonClicked()
    {
        ApplyFreeWorker();
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        jobManager = JobManager.instance;
        workerManager = WorkerManager.instance;
        SetObjective();
    }
    private void Update()
    {
        UpdateTick();
    }

    void UpdateTick()
    {
        jobManager.JobsTick();
        workerManager.WorkerTick();
    }

    void SetObjective()
    {
        float modif = 1 + (0.1f * (level - 1));
        jobPointsTotal = Mathf.RoundToInt(150 * level * modif);
    }

    public void AddJobPoints(int points)
    {
        jobPointsCurrent += points;
        if (jobPointsCurrent >= jobPointsTotal)
        {
            jobPointsCurrent = jobPointsTotal;
            UpdateBar();
            //objectivesCompleted++;
            //level++;
            Debug.Log("Objective Completed!");
            return;
        }
        UpdateBar();
        Debug.Log("Job Points: " + jobPointsCurrent + " / " + jobPointsTotal);
    }

    void UpdateBar()
    {
        UIManager.instance.UpdateBar((float)jobPointsCurrent / jobPointsTotal);
    }
    public void ApplyFreeWorker()
    {
        Job freeJob = jobManager.GetFreeJob();
        freeJob.AssignWorker(workerManager.GetFreeWorker());
    }
}
