using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    [Header("Init Card Config")] 
    //public List<CardData> cardDatabase;
    private List<CardData> _deck;
    public DeckData deckData;
    public DeckRuntime currentDeck;
    
    //public List<DeckData> deck;
    
    [Header("Dependency")]
    public HandManager handManager;
    //[SerializeField] private CardDisplay cardDisplay;



    private void Start()
    {
        currentDeck = new DeckRuntime();
        currentDeck.LoadFromTemplate(deckData);
        currentDeck.Shuffle();
        
        DrawInitialCards(5);
    }

    private void Update()
    {
        
    }

    private void DrawInitialCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardData card = currentDeck.Draw();
            if (card != null)
            {
                HandManager.Instance.AddCardToHand(card);
            }
        }
    }

    public void DrawCard()
    {
        CardData topCard = currentDeck.Draw();
        if (topCard != null)
        {
            HandManager.Instance.AddCardToHand(topCard);
        }
    }

    public void AddNewCard(CardData newCard)
    {
        currentDeck.AddCard(newCard);
    }

    public void ResetGame()
    {
        Debug.Log("Deck has been reset, run Reset Game Logic here.");
        currentDeck.LoadFromTemplate(deckData);
        currentDeck.Shuffle();
    }
    
    // for enemy AI
    public CardData DrawCardDirect()
    {
        if (_deck.Count == 0) return null;

        CardData topCard = _deck[0];
        _deck.RemoveAt(0);
        return topCard;
    }
}
