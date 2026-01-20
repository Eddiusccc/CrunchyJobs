using UnityEngine;

public class JobManager : MonoBehaviour
{
    public static JobManager instance;

    public Job[] jobs;
    public static float jobTickTimer_default;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        jobs = GameObject.FindObjectsByType<Job>(FindObjectsSortMode.None);
    }
    private void Update()
    {
        JobsTick();

    }

    public void JobsTick()
    {
        foreach (Job job in jobs)
        {
            if (job.jobState == JobState.Ocupado)
            {
                job.JobTickUpdate();
            }
        }
    }
}
