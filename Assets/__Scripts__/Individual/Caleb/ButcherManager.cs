using UnityEngine;

public class Butcher : MonoBehaviour
{
    [Header("References")]
    public DeckRuntime playerDeckRuntime;  // Reference to the player's runtime deck
    public CardData meatCard;              // The meat card to add

    void Start()
    {
        // Optional: Add meat cards at start for testing
        // AddMeatCards(2);
    }

    /// <summary>
    /// Adds a specified number of meat cards to the runtime deck.
    /// </summary>
    public void AddMeatCards(int amount)
    {
        if (playerDeckRuntime == null || meatCard == null)
        {
            Debug.LogWarning("Butcher: Missing deck or meat card reference.");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            playerDeckRuntime.AddCard(meatCard);
        }

        Debug.Log($"{amount} Meat card(s) added to the deck.");
    }

    /// <summary>
    /// Removes the currently selected card from the runtime deck.
    /// </summary>
    public void RemoveSelectedCard()
    {
        var selected = SelectCardManager.Instance.currentCard;

        if (selected != null && playerDeckRuntime != null)
        {
            playerDeckRuntime.RemoveCard(selected.cardData);
            Debug.Log("Removed selected card from the deck: " + selected.cardData.CardName);
        }
        else
        {
            Debug.LogWarning("No card selected or deck not set.");
        }
    }
}