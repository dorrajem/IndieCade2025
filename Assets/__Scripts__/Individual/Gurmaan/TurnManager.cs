using System.Collections;
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
    public DisasterManager disaster;

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

    public IEnumerator EnemyTurn()
    {
        if (gameTurn != GameTurn.EnemyTurn) yield return new WaitForSeconds(0f);
        yield return new WaitForSeconds(0.25f);
        enemyAI.EnemyTakeTurn(this);
    }
    
    // Starts the Player's turn
    public void TurnStart()
    {
        cardDeck.drawn = false;
        resourceManagement.AddPoints(1);
        gameTurn = GameTurn.PlayerTurn;
        turnCount++;
        if (turnCount >= 2)
        {
            disaster.disasterCount--;
        }
    }
}


