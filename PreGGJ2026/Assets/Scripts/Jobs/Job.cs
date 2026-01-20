using UnityEngine;

public class Job : MonoBehaviour
{
    [Header("JOB DATA")]
    [Space(10)]
    public string jobName;
    public JobClass jobClass;
    public JobType jobType;
    [Header("WORKER ON JOB VALUES")]
    [Space(10)]
    public Worker currentWorker;





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
