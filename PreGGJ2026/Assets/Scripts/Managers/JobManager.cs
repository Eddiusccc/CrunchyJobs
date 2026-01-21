using UnityEngine;

public class JobManager : MonoBehaviour
{
    public static JobManager instance;

    public Job[] allJobs;
    public float jobTickTimer_total = 5f;
    public static float jobTickTimer_default;

    private void Awake()
    {
        instance = this;
        jobTickTimer_default = jobTickTimer_total;
    }

    private void Start()
    {
        allJobs = GameObject.FindObjectsByType<Job>(FindObjectsSortMode.None);
    }

    public Job GetFreeJob()
    {
        foreach (Job job in allJobs)
        {
            if (job.jobState == JobState.Disponible)
            {
                return job;
            }
        }
        return null;
    }

    public void JobsTick()
    {
        foreach (Job job in allJobs)
        {
            if (job.jobState == JobState.Ocupado)
            {
                job.JobTickUpdate();
            }
        }
    }
}
