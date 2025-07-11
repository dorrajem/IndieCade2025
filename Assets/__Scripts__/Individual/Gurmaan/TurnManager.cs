using UnityEngine;

public enum GameTurn
{
    PlayerTurn,
    EnemyTurn,
    PlayerCard,
    EnemyCard,
}
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public ResourceManagement resourceManagement;
    //public OpponentManager opponent;
    public CardDeck cardDeck;
    public EnemyAIController enemyAI;
    public GameTurn gameTurn;

    public int turnCount = 0; //Caleb's Edit

    // Ends the player's current turn
    void Awake()
    {
        gameTurn = GameTurn.EnemyTurn;
        Instance = this;
    }
    
    public void EndTurn()
    {
        if (gameTurn != GameTurn.PlayerTurn) return;
        gameTurn = GameTurn.PlayerCard;
    }

    public void EnemyTurn()
    {
        if (gameTurn != GameTurn.EnemyTurn) return;
        enemyAI.EnemyTakeTurn(this);
    }
    
    // Starts the Player's turn
    public void TurnStart()
    {
        cardDeck.drawn = false;
        resourceManagement.AddPoints(2);
        gameTurn = GameTurn.PlayerTurn;
        turnCount++;
    }
}


