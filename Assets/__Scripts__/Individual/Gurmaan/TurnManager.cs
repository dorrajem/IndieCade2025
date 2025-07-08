using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public bool IsPlayerTurn = true;
    public ResourceManagement resourceManagement;
    //public OpponentManager opponent;
    public CardDeck cardDeck;
    public EnemyAIController enemyAI; 

    // Ends the player's current turn
    void Awake()
    {
        TurnStart();
        Instance = this;
    }
    public void EndTurn()
    {
        IsPlayerTurn = false;
        enemyAI.EnemyTakeTurn(this);
    }
    
    // Starts the Player's turn
    public void TurnStart()
    {
        cardDeck.drawn = false;
        resourceManagement.AddPoints(2);
        IsPlayerTurn = true;
    }
}


