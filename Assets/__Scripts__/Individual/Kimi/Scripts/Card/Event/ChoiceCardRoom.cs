using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChoiceCardRoom : MonoBehaviour
{
    public DeckData deckData;
    private const int NumberOfChoice = 3;
    public UICardChoice choiceUI;

    private void Start()
    {
        List<CardData> selectedCards = deckData.cards.
                                       OrderBy(_ => Random.value)
                                       .Take(NumberOfChoice)
                                       .ToList();
        
        choiceUI.ShowChoices(selectedCards, OnCardChosen);
    }

    private void OnCardChosen(CardData selected)
    {
        GameSession.Instance.PlayerDeck.AddCard(selected);
        Debug.Log("Added Card: " + selected);
        Debug.Log(GameSession.Instance.PlayerDeck);
        Debug.Log("====== Player's Current Deck ======");
        foreach (var card in GameSession.Instance.PlayerDeck.GetAllCards())
        {
            Debug.Log($"- {card.CardName} (CardCategory: " +
                      $"{card.cardCategory}, CostCheck: {card.SaplingCostPoint})");
        }
        Debug.Log("====================================");
        // load next scene or return to world map
        // SceneManager.LoadScene("WorldMap")
    }
}
