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
    private GameSceneManager sceneManager;

    private void Start()
    {
        sceneManager = GameObject.FindWithTag("Manager").GetComponent<GameSceneManager>();
        List<CardData> selectedCards = deckData.cards.
                                       OrderBy(_ => Random.value)
                                       .Take(NumberOfChoice)
                                       .ToList();
        
        choiceUI.ShowChoices(selectedCards, OnCardChosen);
    }

    private void OnCardChosen(CardData selected)
    {
        GameSession.Instance.PlayerDeck.AddCard(selected);
        sceneManager.Map();
    }
}
