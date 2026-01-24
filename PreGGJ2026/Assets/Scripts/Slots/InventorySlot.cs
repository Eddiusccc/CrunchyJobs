using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isJobSlot = false;
    Job job;

    private void Start()
    {
        if (isJobSlot)
        {
            job = GetComponent<Job>();
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        DraggableItem draggableItem = droppedObject.GetComponent<DraggableItem>();
        if(isJobSlot)
        {
            CheckWorker(droppedObject);
        }
        else
        {
            CheckWorkerUnassign(droppedObject);
        }
        if (draggableItem != null)
        {
            draggableItem.parentAfterDrag = transform;
        } 
    }

    void CheckWorker(GameObject go)
    {
        Worker worker = go.GetComponent<Worker>();
        if(worker.currentState == WorkerState.Descansando)
        {
            if(job.jobState == JobState.Disponible)
            {
                job.AssignWorker(worker);
                Debug.Log("Worker assigned to job: " + job.jobName);
            }
        }
    }

    void CheckWorkerUnassign(GameObject go)
    {
        Worker worker = go.GetComponent<Worker>();
        if(worker.currentState == WorkerState.Trabajando)
        {
            Debug.Log("Worker unassigned from job: " + worker.currentJob.jobName);
            worker.currentJob.RemoveWorker();
        }
    }
}