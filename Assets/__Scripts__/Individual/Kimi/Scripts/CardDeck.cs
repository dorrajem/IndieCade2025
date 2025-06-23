using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    [Header("Init Card Config")] 
    public List<CardData> cardDatabase;
    private List<CardData> _deck = new List<CardData>();
    
    [Header("Dependency")]
    public HandManager handManager;
    //[SerializeField] private CardDisplay cardDisplay;
    
    

    private void Start()
    {
        InitializeDeck();
        ShuffleDeck();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    public void InitializeDeck()
    {
        _deck = new List<CardData>(cardDatabase);
        
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < _deck.Count; i++)
        {
            int randomIdex = Random.Range(i, _deck.Count);
            (_deck[i], _deck[randomIdex]) = (_deck[randomIdex], _deck[i]);
        }
    }

    public void DrawCard()
    {
        if (_deck.Count == 0) return;
        CardData topCard = _deck[0];
        _deck.RemoveAt(0);
        
        handManager.AddCardToHand(topCard);
    }
}
