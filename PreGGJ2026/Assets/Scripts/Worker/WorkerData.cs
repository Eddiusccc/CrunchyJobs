using UnityEngine;

[CreateAssetMenu(fileName = "WorkerData", menuName = "ScriptableObjects/WorkerData")]
public class WorkerData : ScriptableObject
{
    public string workerName;
    public int workerID;

    public void Hablar()
    {
        Debug.Log(workerName);
        Debug.Log(workerID);
    }
}
