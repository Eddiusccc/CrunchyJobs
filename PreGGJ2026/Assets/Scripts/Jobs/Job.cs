using UnityEngine;

public class Job : MonoBehaviour
{
    [Header("JOB DATA")]
    [Space(10)]
    public string jobName;
    public JobClass jobClass;
    public JobType jobType;
    public int jobPointsReward = 1;
    public static int JPR_min = 5;
    public static int JPR_max = 20;
    [HideInInspector] public JobState jobState = JobState.Disponible;
    [Header("WORKER ON JOB VALUES")]
    [Space(10)]
    public Worker currentWorker;
    float jobTickTotal = 0f;
    float jobTickCurrent = 0f;

    #region WORKER MANAGEMENT
    public void ResetJob()
    {
        jobTickCurrent = 0f;
        jobTickTotal = JobManager.jobTickTimer_default;
    }
    public void AssignWorker(Worker worker)
    {
        currentWorker = worker;
        currentWorker.currentJob = this;
        worker.currentState = WorkerState.Trabajando;
        jobState = JobState.Ocupado;
        SetJobTickTimer(worker);
    }
    public void RemoveWorker()
    {
        currentWorker.currentJob = null;
        currentWorker.currentState = WorkerState.Descansando;
        currentWorker = null;
        jobState = JobState.Disponible;
        ResetJob();
    }

    public void ForceRemoveWorker()
    {
        currentWorker.currentJob = null;
        currentWorker.currentState = WorkerState.NoDisponible;
        currentWorker = null;
        jobState = JobState.Disponible;
        ResetJob();
    }

    #endregion

    #region TICK MANAGEMENT

    public void JobTickUpdate()
    {
        if (jobTickCurrent < jobTickTotal)
        {
            jobTickCurrent += Time.deltaTime;
            return;
        }
        jobTickCurrent = 0f;
        if (currentWorker == null)
        {
            Debug.LogWarning("Job Tick Update called but no worker assigned to job: " + jobName);
            jobTickCurrent = 0f;
            jobState = JobState.Disponible;
            return;
        }
        if(currentWorker.currentState != WorkerState.Trabajando)
        {
            Debug.LogWarning("Job Tick Update called but worker is not in working state: " + currentWorker.workerName);
            jobTickCurrent = 0f;
            jobState = JobState.Disponible;
            RemoveWorker();
            return;
        }
        OnTickCompleted();
        currentWorker.WorkApply();
    }

    void OnTickCompleted()
    {
        float pointsCalc = 0f;
        switch (jobClass)
        {
            case JobClass.Operaciones:
                pointsCalc = (float)currentWorker.GetWorkerStat(StatType.Operaciones).CurrentValue();
                break;
            case JobClass.Comercio:
                pointsCalc = (float)currentWorker.GetWorkerStat(StatType.Comercio).CurrentValue();
                break;
            case JobClass.Finanzas:
                pointsCalc = (float)currentWorker.GetWorkerStat(StatType.Finanzas).CurrentValue();
                break;
        }

        pointsCalc = pointsCalc.Remap(1, 20, 1.1f, 3.5f);
        int pointsToAdd = Mathf.RoundToInt(pointsCalc * jobPointsReward);

        ObjectiveManager.instance.AddJobPoints(pointsToAdd);
    }

    void SetJobTickTimer(Worker worker)
    {
        jobTickCurrent = 0f;
        float modif = (float)worker.GetWorkerStat(StatType.Velocidad).CurrentValue();
        jobTickTotal = JobManager.jobTickTimer_default * modif.Remap(1, 20, 1f, 0.66f);
        Debug.Log("Job Tick Timer set to: " + jobTickTotal + " seconds.");
    }


    #endregion


    private void Start()
    {
        SetJobPointsReward(jobPointsReward);
    }
    private void OnValidate()
    {
        SetJobClass(jobType);
        jobName = GetJobFullName();
    }
    public string GetJobFullName()
    {
        return jobClass.ToString() + " - " + jobType.ToString();
    }
    public void SetJobClass(JobType jobType)
    {
        jobClass = GetJobClass(jobType);
    }
    public JobClass GetJobClass(JobType jobType)
    {
        switch (jobType)
        {
            case JobType.SoporteTecnico:
            case JobType.Mantenimiento:
            case JobType.Logistica:
            case JobType.Ingenieria:
                return JobClass.Operaciones;
            case JobType.Ventas:
            case JobType.Marketing:
            case JobType.RelacionesPublicas:
            case JobType.Contactos:
                return JobClass.Comercio;
            case JobType.Contabilidad:
            case JobType.Auditoria:
            case JobType.Inversiones:
            case JobType.Analisis:
                return JobClass.Finanzas;
            default:
                return JobClass.Operaciones;
        }
    }
    public void SetJobPointsReward(int jpr)
    {
        jobPointsReward = Mathf.Clamp(jpr, JPR_min, JPR_max);
    }
}

public enum JobClass
{
    Operaciones,
    Comercio,
    Finanzas
}
public enum JobType
{
    SoporteTecnico,
    Mantenimiento,
    Logistica,
    Ingenieria,

    Ventas,
    Marketing,
    RelacionesPublicas,
    Contactos,

    Contabilidad,
    Auditoria,
    Inversiones,
    Analisis
}
public enum JobState
{
    Disponible,
    Ocupado
}
