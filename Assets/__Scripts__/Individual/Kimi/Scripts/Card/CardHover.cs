using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hover Settings")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private bool hoverable = true;

    [Header("Selection Settings")]
    [SerializeField] private Vector2 selectedAnchoredPosition = new Vector2(0, 200);
    [SerializeField] private float selectScale = 1.2f;
    [SerializeField] private float transitionSpeed = 10f;
    [SerializeField] private float handDropAmount = 50f;

    private RectTransform handArea;
    private Vector3 originalScale;
    private Vector2 originalPosition;
    private Vector2 originalHandPosition;
    private Transform originalParent;
    private Vector2 targetAnchoredPos;

    private bool isSelected = false;
    private bool isMovingToPreview = false;

    public static CardHover currentlySelected;

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        originalScale = transform.localScale;
        handArea = GameObject.FindWithTag("Hand")?.GetComponent<RectTransform>();
        if (handArea != null)
            originalHandPosition = handArea.anchoredPosition;
    }

    private void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(1))
        {
            DeselectCard();
        }

        transform.localScale = isSelected ? originalScale * selectScale : originalScale;

        if (isMovingToPreview)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetAnchoredPos, Time.deltaTime * transitionSpeed);
            if (Vector2.Distance(rt.anchoredPosition, targetAnchoredPos) < 0.1f)
            {
                rt.anchoredPosition = targetAnchoredPos;
                isMovingToPreview = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected || eventData.button != PointerEventData.InputButton.Left) return;

        if (currentlySelected != null && currentlySelected != this)
            currentlySelected.DeselectCard();

        SelectCard();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hoverable || isSelected) return;
        transform.localScale = originalScale * hoverScale;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!hoverable || isSelected) return;
        transform.localScale = originalScale;
    }

    #region SelectCard

    private void SelectCard()
    {
        isSelected = true;
        currentlySelected = this;
        SelectCardManager.Instance.SelectCard(GetComponent<CardDisplay>());

        originalParent = transform.parent;
        originalPosition = rt.anchoredPosition;

        targetAnchoredPos = selectedAnchoredPosition;
        isMovingToPreview = true;

        if (handArea != null)
            handArea.anchoredPosition = originalHandPosition - new Vector2(0, handDropAmount);
    }

    private void DeselectCard()
    {
        isSelected = false;
        if (currentlySelected == this) currentlySelected = null;

        transform.SetParent(originalParent);
        targetAnchoredPos = originalPosition;
        isMovingToPreview = true;

        if (handArea != null)
            handArea.anchoredPosition = originalHandPosition;
    }

    #endregion
}
