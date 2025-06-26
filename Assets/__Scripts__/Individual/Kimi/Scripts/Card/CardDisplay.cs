using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image cardImage;
    
    [Header("Selection Settings")]
    [SerializeField] private Vector2 selectedAnchoredPosition = new Vector2(0, 200); 
    [SerializeField] private float selectScale = 1.2f;
    [SerializeField] private float transitionSpeed = 10f;
    [SerializeField] private float handDropAmount = 50f;

    [Header("References")]
    private RectTransform handArea; 

    private Vector3 originalScale;
    private Vector3 currScale;
    private Vector2 originalPosition;
    private Vector2 originalHandPosition;
    private Transform originalParent;
    
    private bool isSelected = false;
    private bool isMovingToPreview = false;
    private Vector2 targetAnchoredPos;

    public CardData cardData;
    public static CardDisplay currentlySelected;

    private void Awake()
    {
        handArea = GameObject.FindWithTag("Hand").GetComponent<RectTransform>();
        if (handArea != null)
        {
            originalHandPosition = handArea.anchoredPosition;
        }
    }

    public void Setup(CardData data)
    {
        cardData = data;
        originalScale = transform.localScale;
        currScale = originalScale;
        cardImage.sprite = cardData.Artwork;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected || eventData.button != PointerEventData.InputButton.Left) 
            return;

        if (currentlySelected != null && currentlySelected != this)
        {
            currentlySelected.DeselectCard();
        }

        SelectCard();
    }

    private void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(1))
        {
            DeselectCard();
        }

        transform.localScale = currScale;
        
        if (isMovingToPreview)
        {
            RectTransform rt = (RectTransform)transform;
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetAnchoredPos, Time.deltaTime * transitionSpeed);

            if (Vector2.Distance(rt.anchoredPosition, targetAnchoredPos) < 0.1f)
            {
                rt.anchoredPosition = targetAnchoredPos;
                isMovingToPreview = false;
            }
        }
    }
    
    private void SelectCard()
    {
        isSelected = true;
        currentlySelected = this;
        originalParent = transform.parent;
        originalPosition = ((RectTransform)transform).anchoredPosition;
        
        targetAnchoredPos = selectedAnchoredPosition;
        currScale = originalScale * selectScale;
        isMovingToPreview = true;

        // Move entire hand area down
        if (handArea != null)
        {
            handArea.anchoredPosition = originalHandPosition - new Vector2(0, handDropAmount);
        }
    }
    
    private void DeselectCard()
    {
        isSelected = false;
        if (currentlySelected == this) currentlySelected = null;
        
        transform.SetParent(originalParent);
        targetAnchoredPos = originalPosition;
        currScale = originalScale;
        isMovingToPreview = true;

        if (handArea != null)
        {
            handArea.anchoredPosition = originalHandPosition;
        }
    }
}