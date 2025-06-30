using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn = true;
    public ResourceManagement resourceManagement;
    public OpponentManager opponent;
    public CardDeck cardDeck;

    // Ends the player's current turn
    void Awake()
    {
        TurnStart();
    }
    public void EndTurn()
    {
        IsPlayerTurn = false;
        opponent.isOpponentTurn = true;
    }
    
    // Starts the Player's turn
    public void TurnStart()
    {
        cardDeck.drawn = false;
        resourceManagement.AddPoints(2);
        IsPlayerTurn = true;
    }
}


