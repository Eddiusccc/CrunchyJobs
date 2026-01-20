using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Draggable draggable = dropped.GetComponent<Draggable>();
            if(draggable != null)
            {
                draggable.parentAfterDrag = transform;
                eventData.pointerDrag.transform.SetParent(this.transform);
            }
        }
    }
}
