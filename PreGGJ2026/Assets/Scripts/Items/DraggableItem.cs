using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Cryptography.X509Certificates;

public class DraggableItem : MonoBehaviour, 
    IBeginDragHandler, 
    IDragHandler, 
    IEndDragHandler
{
    // Reference to the Canvas component
    [SerializeField] private Canvas canvas;
    // Reference to the RectTransform component
    private RectTransform rectTransform;
    //Reference to the parent to return to after drag
    [HideInInspector] public Transform parentAfterDrag;
    //Reference to Image for disable and enabling RayCast
    public Image image;
    //Reference for knowing the parent slot position
    public string parentSlotItemIsIn;
    public string parentSlotItemWasIn;

    private void Awake()
    {
        //Assign the canvas component
        canvas = GetComponentInParent<Canvas>();
        //Assign the RectTransform component
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        UpdateParentSlotItemWasIn();
        //Disable canvas to avoid checks in the drop moment
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        UpdateParentSlotItemIsIn();
    }
    //this will tell you the parent slot name where the item is already, not the canvas
    public string KnowParentSlot()
    {
        Transform parentTransform = this.transform.parent;
        string parentName = parentTransform.gameObject.name;
        return parentName;
    }
    //this will update the parent slot name position where the item is in
    public void UpdateParentSlotItemIsIn()
    {
        parentSlotItemIsIn = KnowParentSlot();
    }
    //this will update the parent slot name position where the item was in
    public void UpdateParentSlotItemWasIn()
    {
        parentSlotItemWasIn = KnowParentSlot();
    }
}
