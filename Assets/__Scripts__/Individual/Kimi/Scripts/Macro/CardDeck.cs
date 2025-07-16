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

    public TurnManager turnManager; //Caleb's Edit
    public bool drawn = false; //Gurmaan's edit

    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindWithTag("Manager").GetComponent<AudioManager>();
        currentDeck = new DeckRuntime();
        currentDeck.LoadFromTemplate(deckData);
        currentDeck.Shuffle();
        
        DrawInitialCards(4);

        //Caleb's Edit
        if (turnManager == null)
        {
            turnManager = UnityEngine.Object.FindFirstObjectByType<TurnManager>();
        }
    }
    

    private void DrawInitialCards(int count)
    {
        CardData firstCard= currentDeck.DrawInit();
        HandManager.Instance.AddCardToHand(firstCard);
        
        for (int i = 0; i < count-1; i++)
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
        if (turnManager == null || turnManager.gameTurn!=GameTurn.PlayerTurn || drawn) return; //Caleb's Edit

        CardData topCard = currentDeck.Draw();
        if (topCard != null)
        {
            HandManager.Instance.AddCardToHand(topCard);
            drawn = true;
            audioManager.PlayCardSelect();
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
