using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;

    private bool isUICard = false;
    
    [SerializeField] private CardUI _cardUI;
    [SerializeField] private Card2D _card2D;


    private void Awake()
    {
        if (isUICard)
        {
            _cardUI = GetComponent<CardUI>();
            if (_cardUI == null)
            {
                Debug.LogWarning("card is ui, but no CardUI");
            }
        }
    }

    public void Setup(CardData data)
    {
        
    }

    public void Init(CardData newData, CardState newState)
    {
        cardData = newData;
        cardData.cardState = newState;
        
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // UI, Artwork, Name, Cost
    }

    public CardData GetCardData() => cardData;

    public void PlayToBoard(Vector3 pos)
    {
        HandManager.Instance.PlayCardToWorld(this, pos);
    }

}