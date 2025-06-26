/*using UnityEngine;
using UnityEngine.EventSystems;
public class CardDragHandler : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Transform originalParent;
    private Vector2 originalPosition;
    private bool droppedOnValidZone = false;

    
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
        HandManager.Instance.StartDrag(gameObject);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        HandManager.Instance.UpdateDrag(gameObject);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        transform.SetParent(originalParent);

        if (HandManager.Instance.currentPlaceholder == null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }

        HandManager.Instance.EndDrag(gameObject);
    }

    
    public void SnapTo(Transform newParent)
    {
        droppedOnValidZone = true;
        transform.SetParent(newParent);
        rectTransform.anchoredPosition = Vector2.zero; 
    }
}*/
