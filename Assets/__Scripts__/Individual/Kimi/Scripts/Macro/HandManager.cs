using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;
    
    [Header("Layout Config")]
    public Transform origin;
    public RectTransform handArea;
    //public DropArea dropArea;
    public GameObject cardPrefab;
    public GameObject cardPrefab2D;
    
    // For later anim
    //public GameObject placeholderPrefab;
    //public GameObject currentPlaceholder;
    private List<Vector2> slotPosition = new();
    

    [Header("Spacing Setting")] 
    public float defaultSpacing = 160f;
    public float cardMinSpacing = 60f;
    
    private List<GameObject> handCards = new ();

    [ReadOnly]public Card currentCard;
    
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Manager").GetComponent<AudioManager>();
        // Keep singleton
        // Remember to create another manager to manage all singletons
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    

    
    // Needs revision for future card types
    // TODO: DOTween
    #region CardDrag
    
    public void AddCardToHand(CardData cardData)
    {
        GameObject card = Instantiate(cardPrefab, handArea);
        card.GetComponent<Card>().Init(cardData, true);
        handCards.Add(card);
        UpdateCardLayout(smooth:false);
    }
    
    // Needs decoupling here
    // TODO: create UpdateCardLayout(Gameobject ignore = null, bool useSmooth = false)
    public void UpdateCardLayout(GameObject ignore = null, bool smooth = true)
    {
        int count = handCards.Count;
        if (count == 0) return;
        
        UpdateSlotPositions(count);

        for (int i = 0; i < count; i++)
        {
            GameObject card = handCards[i];
            if (card == ignore) continue;

            RectTransform rt = card.GetComponent<RectTransform>();
            Vector2 targetPos = slotPosition[i];

            rt.anchoredPosition = smooth ? Vector2.Lerp(rt.anchoredPosition, targetPos, 0.3f) : targetPos;
            card.transform.SetSiblingIndex(i);
        }
    }

    private void UpdateSlotPositions(int count)
    {
        slotPosition.Clear();

        float areaWidth = handArea.rect.width;
        float spacing = Mathf.Min(defaultSpacing, areaWidth / Mathf.Max(1, count - 1));
        spacing = Mathf.Max(cardMinSpacing, spacing);

        float totalWidth = spacing * (count - 1);
        float startX = -totalWidth / 2f;
        Vector2 originPos = origin.localPosition;

        for (int i = 0; i < count; i++)
        {
            float x = startX + i * spacing;
            slotPosition.Add(new Vector2(originPos.x + x, originPos.y));
        }
    }
    
    #endregion
    
    #region ManageHandAcrossLevels

    public void ResetHand()
    {
        // Logics here...
    }
    
    public void RemoveCardFromHand(GameObject card)
    {
        if (handCards.Contains(card))
        {
            handCards.Remove(card);
            Destroy(card);
            UpdateCardLayout();
        }
    }

    #endregion


    #region PlayCard

    public GameObject PlayCardToWorld(Card selectedCard, Vector3 slotPos, CardOwner setOwner, int Index)
    {
        if (cardPrefab2D == null)
        {
            Debug.Log("Card prefab is not assigned");
            return null;
        }
        audioManager.PlayCardPlace();
        
        selectedCard.DeselectVisual();
        
        CardData cardData = selectedCard.GetCardData();
        handCards.Remove(selectedCard.gameObject);
        Destroy(selectedCard.gameObject);
        
        GameObject newCardObj = Instantiate(cardPrefab2D, slotPos, Quaternion.identity);
        // scale saver
        newCardObj.transform.localScale = Vector3.one;
        Card card = newCardObj.GetComponent<Card>();
        card.cardState = CardState.OnTable;
        card.cardOwner = setOwner;
        card.Init(cardData, false);
        
        UpdateCardLayout(smooth: true);
        
        BoardManager.Instance.RegisterCard(card,Index);
        return newCardObj;
    }

    public GameObject ChangeCardToWorld(CardData cardData, Vector3 slotPos, CardOwner setOwner, int Index)
    {
        audioManager.PlayCardPlace();
        
        GameObject newCardObj = Instantiate(cardPrefab2D, slotPos, Quaternion.identity);
        // scale saver
        newCardObj.transform.localScale = Vector3.one;
        Card card = newCardObj.GetComponent<Card>();
        card.cardState = CardState.OnTable;
        card.cardOwner = setOwner;
        card.Init(cardData, false);
        
        UpdateCardLayout(smooth: true);
        
        BoardManager.Instance.RegisterCard(card,Index);
        return newCardObj;
    }


    #endregion
}
