using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hover Settings")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private bool hoverable = true;

    [Header("Selection Settings")]
    [SerializeField] private Vector2 selectedAnchoredPosition = new Vector2(-20, 500);
    [SerializeField] private float selectScale = 1.4f;
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

    public TurnManager turnManager; //Caleb's Edit
    public SacrificeManager sacrificeManager;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Manager").GetComponent<AudioManager>();
        rt = GetComponent<RectTransform>();
        originalScale = transform.localScale;
        handArea = GameObject.FindWithTag("Hand")?.GetComponent<RectTransform>();
        if (handArea != null)
            originalHandPosition = handArea.anchoredPosition;

        //Caleb's Edit
        // Automatically find EndTurnManager if not set
        if (turnManager == null)
        {
            turnManager = Object.FindFirstObjectByType<TurnManager>();
        }
        sacrificeManager = GameObject.FindWithTag("Manager").GetComponent<SacrificeManager>();
    }

    private void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(1) &&  !sacrificeManager.CanPlace)
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

        if (turnManager.gameTurn == GameTurn.EnemyCard || turnManager.gameTurn == GameTurn.PlayerCard)
        {
            handArea.anchoredPosition = originalHandPosition - new Vector2(0, handDropAmount);
        }
        else if (turnManager.gameTurn == GameTurn.EnemyTurn)
        {
            handArea.anchoredPosition = originalHandPosition;
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected || eventData.button != PointerEventData.InputButton.Left || sacrificeManager.CanPlace) return;

        // Only allow selection if it's the player's turn
        if (turnManager == null || turnManager.gameTurn!=GameTurn.PlayerTurn) return; //Caleb's Edit

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
        audioManager.PlayCardSelect();
        isSelected = true;
        currentlySelected = this;
        SelectCardManager.Instance.SelectCard(GetComponent<Card>());

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
        SelectCardManager.Instance.ClearSelection();
    }

    public void ForceDeselect()
    {
        if (isSelected && !sacrificeManager.CanPlace)
        {
            DeselectCard();
        }
    }

    #endregion
}
