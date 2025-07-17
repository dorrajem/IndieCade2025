using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardChoiceRoom : MonoBehaviour
{
    public DeckData cardDeck;
    public const int NumberOfChoices = 3;

    private void Start()
    {
        var choices = cardDeck.cards.OrderBy(x => Random.value).Take(NumberOfChoices).ToList();
        
        // Display card choice UI here
    }

    void OnCardChosen(CardData chosenCard)
    {
        GameSession.Instance.PlayerDeck.AddCard(chosenCard);
        
        Debug.Log("Card chosen: " + chosenCard);
        // After card chosen, go back to map scene for the next move
        // SceneManager.LoadScene("MapScene")
    }
}
