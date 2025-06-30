using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn = true;
    public ResourceManagement resourceManagement;
    public CardDeck cardDeck;

    // Ends the player's current turn
    public void EndTurn()
    {
        IsPlayerTurn = false;
    }
    
    // Starts the Player's turn
    public void TurnStart()
    {
        cardDeck.drawn = false;
        resourceManagement.AddPoints(2);
    }
}


